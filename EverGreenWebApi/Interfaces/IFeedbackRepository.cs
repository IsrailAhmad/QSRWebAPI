using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public interface IFeedbackRepository : IDisposable
    {
        FeedbackDetailsModel AddFeedback(FeedbackModel model);
        IEnumerable<FeedbackDetailsModel> GetAllFeedback(FeedbackDetailsModel model);
    }
}