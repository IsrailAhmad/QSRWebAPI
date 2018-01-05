using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EverGreenWebApi.Models;
using EverGreenWebApi.DBHelper;
using System.Net;

namespace EverGreenWebApi.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string GeneratePromoCode()
        {
            int _min = 1000;
            int _max = 9999;
            string strOrderNumber = string.Empty;
            Random _rdm = new Random();
            int rand = _rdm.Next(_min, _max);
            return strOrderNumber = "QSR" + rand;
        }

        public void SendSMS(string phonenumber, string promocode)
        {


            string _user = HttpUtility.UrlEncode("shamsweet"); // API user name to send SMS
            string _pass = HttpUtility.UrlEncode("12345");     // API password to send SMS
            string _route = HttpUtility.UrlEncode("transactional");
            string _senderid = HttpUtility.UrlEncode("WISHHH");
            string _recipient = HttpUtility.UrlEncode(phonenumber);  // who will receive message
            string _messageText = HttpUtility.UrlEncode("Thanks for Feedback. Yours QSR Promocode is :" + promocode); // text message

            // Creating URL to send sms
            string _createURL = "http://www.smsnmedia.com/api/push?user=" + _user + "&pwd=" + _pass + "&route=" + _route + "&sender=" + _senderid + "&mobileno=91" + _recipient + "&text=" + _messageText;

            HttpWebRequest _createRequest = (HttpWebRequest)WebRequest.Create(_createURL);
            // getting response of sms
            HttpWebResponse myResp = (HttpWebResponse)_createRequest.GetResponse();
            System.IO.StreamReader _responseStreamReader = new System.IO.StreamReader(myResp.GetResponseStream());
            string responseString = _responseStreamReader.ReadToEnd();
            _responseStreamReader.Close();
            myResp.Close();
        }

        public FeedbackDetailsModel AddFeedback(FeedbackModel model)
        {
            FeedbackDetailsModel data = null;
            using (qsr_androidEntities context = new qsr_androidEntities())
            {
                string strPromoCode = GeneratePromoCode();
                feedbackmaster user = new feedbackmaster();
                //user.FeedbackId = model.FeedbackId;
                user.CustomerId = model.CustomerId;
                user.Feedback = model.Feedback;
                user.StoreId = model.StoreId;
                context.feedbackmasters.Add(user);
                var result = context.SaveChanges();
                if (result > 0)
                {
                    if (model.CustomerType == "Registered")
                    {

                    }
                    else
                    {
                        promocodemaster p = new promocodemaster();
                        p.CustomerId = model.CustomerId;
                        p.PromoCode = strPromoCode;
                        context.promocodemasters.Add(p);
                        context.SaveChanges();
                        //var promo = (from pro in context.promocodemasters
                        //             where pro.CustomerId == model.CustomerId
                        //             select pro.PromoCode).FirstOrDefault();
                        var customer = (from cust in context.loginmasters
                                        where cust.CustomerId == model.CustomerId
                                        select cust).FirstOrDefault();
                        SendSMS(customer.MobileNumber, strPromoCode);
                    }

                    var serv = (from s in context.loginmasters
                                join sl in context.feedbackmasters on s.CustomerId equals sl.CustomerId
                                where s.CustomerId == model.CustomerId
                                select new FeedbackDetailsModel()
                                {
                                    CustomerId = s.CustomerId,
                                    Feedback = (decimal)sl.Feedback,
                                    FeedbackId = sl.FeedbackId,
                                    StoreId = (int)sl.StoreId,
                                    MobileNumber = s.MobileNumber,
                                    CreatedOn = (DateTime)sl.CreatedOn
                                }).FirstOrDefault();
                    data = serv;
                }
            }
            return data;
        }

        public IEnumerable<FeedbackDetailsModel> GetAllFeedback(FeedbackDetailsModel model)
        {
            IEnumerable<FeedbackDetailsModel> data = null;
            using (qsr_androidEntities context = new qsr_androidEntities())
            {
                feedbackmaster user = new feedbackmaster();
                var serv = (from s in context.loginmasters
                            join sl in context.feedbackmasters on s.CustomerId equals sl.CustomerId
                            where s.CustomerId == model.CustomerId && s.StoreId == model.StoreId
                            orderby sl.CreatedOn descending
                            select new FeedbackDetailsModel()
                            {
                                CustomerId = s.CustomerId,
                                Feedback = (decimal)sl.Feedback,
                                FeedbackId = sl.FeedbackId,
                                StoreId = (int)sl.StoreId,
                                MobileNumber = s.MobileNumber,
                                CreatedOn = (DateTime)sl.CreatedOn

                            }).ToList();
                data = serv;

            }
            return data;
        }
    }
}