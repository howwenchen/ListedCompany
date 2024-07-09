using System.Linq.Expressions;
using AutoMapper;
using ListedCompany.Services.Repository.UnitOfWork;

namespace ListedCompany.Services
{
    /// <summary>
    /// 通用行的Service layer實作
    /// </summary>
    /// <typeparam name="T">主要的Entity形態</typeparam>
    public class GenericService<T> : IService<T> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 取得符合條件的Entity並且轉成對應的ViewModel
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="predicate">過濾邏輯</param>
        /// <returns>取得轉換過的ViewModel List</returns>
        public async Task<List<TViewModel>> GetListToViewModelAsync<TViewModel>(Expression<Func<T, bool>>? predicate = null)
        {
            var data = await _unitOfWork.Repository<T>().GetAllAsync(predicate);
            
            return _mapper.Map<List<TViewModel>>(data);
        }

        /// <summary>
        /// 依照某一個ViewModel的值，更新對應的Entity
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的值</param>
        /// <param name="predicate">過濾條件 - 要被更新的那一筆過濾條件</param>
        /// <returns>是否更新成功</returns>
        public async Task<bool> UpdateViewModelToDatabaseAsync<TViewModel>(TViewModel viewModel, Expression<Func<T, bool>> predicate)
        {
            var entity = await _unitOfWork.Repository<T>().GetAsync(predicate);

            if (entity == null)
            {
                return false;
            }

            _mapper.Map(viewModel, entity);

            await _unitOfWork.Repository<T>().UpdateAsync(entity);

            await _unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// 刪除某一筆Entity
        /// </summary>
        /// <param name="predicate">過濾出要被刪除的Entity條件</param>
        /// <returns>是否刪除成功</returns>
        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _unitOfWork.Repository<T>().GetAsync(predicate);

            if (entity == null)
            {
                return false;
            }

            await _unitOfWork.Repository<T>().RemoveAsync(entity);

            await _unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// 依照某一個ViewModel的值，產生對應的Entity並且新增到資料庫
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        /// <returns>是否儲存成功</returns>
        public async Task<bool> CreateViewModelToDatabaseAsync<TViewModel>(TViewModel viewModel)
        {
            var entity = _mapper.Map<T>(viewModel);

            await _unitOfWork.Repository<T>().AddAsync(entity);

            await _unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// 執行預存程序並返回查詢結果
        /// </summary>
        /// <typeparam name="TResult">回傳資料的型別</typeparam>
        /// <param name="storedProcedure">預存程序名稱</param>
        /// <param name="parameters">預存程序參數</param>
        /// <returns>預存程序執行結果</returns>
        public async Task<IEnumerable<TResult>> QuerySPFromsql<TResult>(string storedProcedure, object parameters = null)
        {
            return await _unitOfWork.Repository<T>().ExecuteSPQuery<TResult>(storedProcedure, parameters);
        }

        /// <summary>
        /// 執行預存程序，不返回查詢結果
        /// </summary>
        /// <param name="storedProcedure">預存程序名稱</param>
        /// <param name="parameters">預存程序參數</param>
        /// <returns>是否執行成功</returns>
        public async Task<bool> ExecuteSPFromsql(string storedProcedure, object parameters = null)
        {
            await _unitOfWork.Repository<T>().ExecuteSP(storedProcedure, parameters);
            return true;
        }
    }
}
