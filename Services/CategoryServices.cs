using System.Net.Http.Json;
using ViewModel.ViewModels;

namespace webapp.Services
{
    public interface ICategoryServices
    {
        Task<List<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> Get(long Id);
        Task<CategoryViewModel> Add(CategoryViewModel model);
        Task<CategoryViewModel> Update(CategoryViewModel model);
        Task<CategoryViewModel> Remove(long Id);
    }
    public class CategoryServices : ICategoryServices
    {

        HttpClient http;

        public CategoryServices(HttpClient _http)
        {
            this.http = _http;
        }


        public async Task<List<CategoryViewModel>> GetAll()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<CategoryViewModel>>("api/Category");
                return response;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoryViewModel> Get(long Id)
        {
            try
            {
                var response = await http.GetFromJsonAsync<CategoryViewModel>("api/Category/" + Id);

                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        public async Task<CategoryViewModel> Add(CategoryViewModel model)
        {
            try
            {
                var response = await http.PostAsJsonAsync<CategoryViewModel>("api/Category" ,model);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await  response.Content.ReadFromJsonAsync<CategoryViewModel>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        public async Task<CategoryViewModel> Update(CategoryViewModel model)
        {
            try
            {
                var response = await http.PutAsJsonAsync<CategoryViewModel>("api/Category/"+ model.Id , model);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryViewModel> Remove(long Id)
        {
            var response = await http.DeleteAsync("api/Category/" + Id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadFromJsonAsync<CategoryViewModel>();
            }
            else
            {
                return null;
            }
        }
    }
}
