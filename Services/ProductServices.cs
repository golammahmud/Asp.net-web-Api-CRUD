using ViewModel.ViewModels;

namespace webapp.Services
{

    public interface IProductServices
    {
        Task<List<ProductViewModel>> GetAll();
        Task<ProductViewModel> Get(long Id);
        Task<ProductViewModel> Add(ProductViewModel model);
        Task<ProductViewModel> Update(ProductViewModel model);
        Task<bool> Remove(long Id);
    }
    public class ProductServices : IProductServices
    {

        HttpClient http;
        public ProductServices(HttpClient _http)
        {
            this.http = _http;
        }



        public async Task<List<ProductViewModel>> GetAll()
        {
            try
            {
                var response = await http.GetFromJsonAsync<List<ProductViewModel>>("api/Product");
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProductViewModel> Get(long Id)
        {
            try
            {
                return await http.GetFromJsonAsync<ProductViewModel>("api/Product/" + Id);
            }
            catch (Exception e)
            {
                throw new ArgumentNullException();
            }

        }

       




        public async Task<ProductViewModel> Add(ProductViewModel model)
        {
            try
            {
                var response = await http.PostAsJsonAsync("api/Product", model);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<ProductViewModel>();

                }
                else
                {
                    return null;
                }
            }
            catch
            {
                throw new NotImplementedException();
            }
            
        }


        public async Task<ProductViewModel> Update(ProductViewModel model)
        {
            try
            {
                var response = await http.PutAsJsonAsync("api/Product/" + model.Id, model);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadFromJsonAsync<ProductViewModel>();

                    // return JsonConvert.DeserializeObject<AppsViewModel>(response.Content.ReadAsStringAsync().Result);

                    //return await result.Content.ReadFromJsonAsync<UserToken>();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<bool> Remove(long Id)
        {
            var response = await http.DeleteAsync("api/Product/" + Id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
