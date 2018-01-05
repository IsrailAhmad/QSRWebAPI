using EverGreenWebApi.Models;
using EverGreenWebApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net;


namespace EverGreenWebApi.Controllers
{
    public class FeedbackController : ApiController
    {
        // GET: Feedback
        //public ActionResult Index()
        //{
        //    return View();
        //}

        static readonly IFeedbackRepository _repository = new FeedbackRepository();

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddFeedback(FeedbackModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.AddFeedback(model);
                if (data != null)
                {
                    response.isSuccess = true;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                }
                else
                {
                    response.isSuccess = false;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllFeedback(FeedbackDetailsModel model)
        {
            ResponseStatus response = new ResponseStatus();
            try
            {
                var data = _repository.GetAllFeedback(model);
                if (data != null)
                {
                    response.isSuccess = true;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.OK, new { data, response });
                }
                else
                {
                    response.isSuccess = false;
                    response.serverResponseTime = System.DateTime.Now;
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { response });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something Worng !", ex);
            }
        }
    }
}