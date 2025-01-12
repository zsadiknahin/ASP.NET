using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.DTOs;
using BLL.Services;

namespace webform.Controllers
{
    public class NewsController : ApiController
    {
        [HttpPost]
        [Route("api/news/add")]
        public HttpResponseMessage Create([FromBody] NewsDTO newsDto)
        {
            try
            {
                var result = NewsService.Create(newsDto);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "News entry created successfully.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to create news entry.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/news/list")]
        public HttpResponseMessage GetAll()
        {
            try
            {
                var data = NewsService.GetAll();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/news/find-by-title/{title}")]
        public HttpResponseMessage GetByTitle(string title)
        {
            try
            {
                var data = NewsService.GetByTitle(title);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/news/find-by-category/{category}")]
        public HttpResponseMessage GetByCategory(string category)
        {
            try
            {
                var data = NewsService.GetByCategory(category);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/news/find-by-date/{date:datetime}")]
        public HttpResponseMessage GetByDate(DateTime date)
        {
            try
            {
                var data = NewsService.GetByDate(date);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/news/find-by-date-and-category")]
        public HttpResponseMessage GetByDateAndCategory([FromUri] DateTime date, [FromUri] string category)
        {
            try
            {
                var data = NewsService.GetByDateAndCategory(date, category);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/news/find-by-date-and-title")]
        public HttpResponseMessage GetByDateAndTitle([FromUri] DateTime date, [FromUri] string title)
        {
            try
            {
                var data = NewsService.GetByDateAndTitle(date, title);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("api/news/edit/{id:int}")]
        public HttpResponseMessage Update(int id, [FromBody] NewsDTO newsDto)
        {
            try
            {
                var result = NewsService.Update(id, newsDto);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "News entry updated successfully.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Failed to update news entry.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/news/remove/{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var result = NewsService.Delete(id);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "News entry deleted successfully.");
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "News entry not found.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
