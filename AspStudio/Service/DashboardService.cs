using EagleApp.Areas.Identity.Data;
using EagleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EagleApp.Service
{
    
    public class DashboardService
    {
        private readonly AspStudioIdentityDbContext _context;
        public DashboardService(AspStudioIdentityDbContext context)
        {
            _context = context;
            
        }

        internal DashBoardModel GetDashboardData(string datatype)
        {
            // List<VDashboardDatum> vDashboardData = null;
            DashBoardModel dashBoardModel = new DashBoardModel();
            if (datatype.ToLower() == "month".ToLower())
            {
                dashBoardModel.VDashboardDatum = _context.VDashboardData.FromSqlRaw("select * from v_dashboard_data  WHERE OpenDate > DATEADD(Month,-1,GETDATE())").ToList();
                dashBoardModel.OpenBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidPrice));
                dashBoardModel.Collected = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.CollectedAmount));
                dashBoardModel.ClosedBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidSales));
            }
            else if (datatype.ToLower() == "year".ToLower())
            {
                dashBoardModel.VDashboardDatum = _context.VDashboardData.FromSqlRaw("select * from v_dashboard_data  WHERE OpenDate > DATEADD(Year,-1,GETDATE())").ToList();
                dashBoardModel.OpenBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidPrice));
                dashBoardModel.Collected = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.CollectedAmount));
                dashBoardModel.ClosedBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidSales));
            }
            else if (datatype.ToLower() == "week".ToLower())
            {
                 dashBoardModel.VDashboardDatum = _context.VDashboardData.FromSqlRaw("select * from v_dashboard_data  WHERE OpenDate > DATEADD(WEEK,-1,GETDATE())").ToList();
               // dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(i => i.OpenDate > DateTime.Now.AddDays(-14)).ToList();
                dashBoardModel.OpenBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidPrice));
                dashBoardModel.Collected = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.CollectedAmount));
                dashBoardModel.ClosedBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidSales));
            }
            dashBoardModel.StartDate = dashBoardModel.EndDate = DateTime.Now;
            return dashBoardModel;

        }

        internal DashBoardModel GetDashboardDataByViewType(string viewType)
        {
            DashBoardModel dashBoardModel = new DashBoardModel();
            if (viewType.ToLower() == "Open Bids".ToLower())
            {
                dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate != null && o.ClosedDate == null && o.Status != "Rejected").ToList();
            }
            else if (viewType.ToLower() == "Closed Bids".ToLower())
            {
                dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.ClosedDate != null).ToList();
            }
            else if (viewType.ToLower() == "Complete Jobs".ToLower())
            {
                dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.FinishDate != null).ToList();
            }
            dashBoardModel.OpenBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Where(o => o.OpenDate != null && o.ClosedDate == null && o.Status != "Rejected").Sum(p => p.EagleBidPrice));
            dashBoardModel.Collected = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Where(o => o.CollectedAmount > 0).Sum(p => p.CollectedAmount));
            dashBoardModel.ClosedBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Where(o => o.ClosedDate != null).Sum(p => p.EagleBidSales));

           // dashBoardModel.StartDate = dashBoardModel.EndDate = DateTime.Now;
            dashBoardModel.ViewType = viewType;
            return dashBoardModel;
        }

        internal DashBoardModel GetDashboardDataByCriteria(DateTime? startDate, DateTime? endDate, string viewType, string viewDataType)
        {
            DashBoardModel dashBoardModel = new DashBoardModel();

            if (!string.IsNullOrEmpty(viewType)){
                if (viewType.ToLower() == "--- SELECT ---".ToLower())
                {
                    viewType = null;
                }
            }

            // Month View - Ability to select All
            if (!string.IsNullOrEmpty(viewDataType))
            {
                if (viewDataType.ToLower() == "--- SELECT ---".ToLower())
                {
                    viewDataType = null;
                }
            }

            // date ranges take precedence over viewDataType
            if (startDate != null && endDate != null)
            {
                viewDataType = null;
            }


            // get base- remove "rejected status" 
            var baseData = _context.VDashboardData.Where(o => o.Status != "6").Where(o=>o.Status != "Rejected").Where(o => o.OpenDate!= null).ToList();
            if(!string.IsNullOrEmpty(viewType) && !string.IsNullOrEmpty(viewDataType))
            {
                #region OLD CODE
                //if (viewType.ToLower() == "Open Bids".ToLower() && viewDataType.ToLower() == "month".ToLower() && startDate != null && endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddMonths(-1) &&
                //                                                                        o.ClosedDate == null).ToList();
                //}
                //if (viewDataType.ToLower() == "month".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate != null)
                //                                             .Where(c => c.OpenDate.Value.Month == DateTime.Now.Month && c.ClosedDate == null).ToList();
                //}


                //else if (viewType.ToLower() == "Open Bids".ToLower() && viewDataType.ToLower() == "year".ToLower() && startDate != null || endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate != null)
                //                                             .Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now &&
                //                                                                        o.ClosedDate == null).ToList();
                //}
                //else if (viewType.ToLower() == "Open Bids".ToLower() && viewDataType.ToLower() == "year".ToLower() && startDate == null && endDate == null)
                //{
                //    // dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate > DateTime.Now && o.ClosedDate == null).ToList();
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate!= null)
                //                                             .Where(c => c.OpenDate.Value.Year == DateTime.Now.Year && c.ClosedDate == null).ToList();
                //}


                //else if (viewType.ToLower() == "Open Bids".ToLower() && viewDataType.ToLower() == "week".ToLower() && startDate != null && endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddDays(-7) &&
                //                                                                       o.ClosedDate == null).ToList();
                //}
                //else if (viewType.ToLower() == "Open Bids".ToLower() && viewDataType.ToLower() == "week".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o =>  o.OpenDate > DateTime.Now.AddDays(-7) &&  o.ClosedDate == null).ToList();
                //}


                //else if (viewType.ToLower() == "Closed Bids".ToLower() && viewDataType.ToLower() == "week".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddDays(-7) &&
                //                                                                       o.CloseoutDoneDate != null).ToList();
                //}
                //else if (viewType.ToLower() == "Closed Bids".ToLower() && viewDataType.ToLower() == "year".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate != null)
                //                                             .Where(c => c.OpenDate.Value.Year == DateTime.Now.Year && c.CloseoutDoneDate != null).ToList();
                //}
                //else if (viewType.ToLower() == "Closed Bids".ToLower() && viewDataType.ToLower() == "month".ToLower() && startDate != null || endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddMonths(-1) &&
                //                                                                        o.CloseoutDoneDate != null).ToList();

                //}

                //else if ((viewType.ToLower() == "Closed Bids".ToLower() && viewDataType.ToLower() == "month".ToLower()) && (startDate == null || endDate == null))
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate != null)
                //                                             .Where(c => c.OpenDate.Value.Month == DateTime.Now.Month && c.CloseoutDoneDate != null).ToList(); //baseData.Where(o =>  o.OpenDate > DateTime.Now.AddMonths(-1) &&  o.ClosedDate != null).ToList();
                //}

                //else if (viewType.ToLower() == "Complete Jobs".ToLower() && viewDataType.ToLower() == "week".ToLower() && startDate != null || endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddDays(-7) &&
                //                                                                       o.PaidInFullDate != null).ToList();
                //}
                //else if (viewType.ToLower() == "Complete Jobs".ToLower() && viewDataType.ToLower() == "week".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate > DateTime.Now.AddDays(-7) && o.PaidInFullDate != null).ToList();
                //} 

                //else if (viewType.ToLower() == "Complete Jobs".ToLower() && viewDataType.ToLower() == "year".ToLower() && startDate != null && endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddYears(-1) &&
                //                                                                        o.PaidInFullDate != null).ToList();
                //}
                //else if (viewType.ToLower() == "Complete Jobs".ToLower() && viewDataType.ToLower() == "year".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate != null)
                //                                             .Where(c => c.OpenDate.Value.Year == DateTime.Now.Year && c.PaidInFullDate != null).ToList(); //baseData.Where(o => o.OpenDate > DateTime.Now.AddYears(-1) && o.CloseoutDoneDate != null).ToList();
                //}

                //else if (viewType.ToLower() == "Complete Jobs".ToLower() && viewDataType.ToLower() == "month".ToLower() && startDate != null && endDate != null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                //                                                                        o.OpenDate <= endDate &&
                //                                                                        o.OpenDate > DateTime.Now.AddMonths(-1) &&
                //                                                                        o.PaidInFullDate != null).ToList();
                //}
                //else if (viewType.ToLower() == "Complete Jobs".ToLower() && viewDataType.ToLower() == "month".ToLower() && startDate == null && endDate == null)
                //{
                //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate != null)
                //                                             .Where(c => c.OpenDate.Value.Month == DateTime.Now.Month && c.PaidInFullDate != null).ToList(); // baseData.Where(o => o.OpenDate > DateTime.Now.AddMonths(-1) && o.CloseoutDoneDate != null).ToList();
                //}
                //else
                //{
                //    // dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate >= startDate && o.OpenDate <= endDate && o.Status != "Rejected").ToList();
                //    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate >= startDate && o.OpenDate <= endDate && o.Status != "Rejected").ToList();
                //}
                #endregion
                if (viewDataType.ToLower() == "month".ToLower() && startDate == null && endDate == null)
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate != null)
                                                             .Where(c => c.OpenDate.Value.Month == DateTime.Now.Month && c.Status == viewType).ToList();
                }
                else if(viewDataType.ToLower() == "year".ToLower() && startDate == null && endDate == null)
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate != null)
                                                             .Where(c => c.OpenDate.Value.Year == DateTime.Now.Year && c.Status == viewType).ToList();
                }
                else if (viewDataType.ToLower() == "week".ToLower() && startDate == null && endDate == null)
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate > DateTime.Now.AddDays(-7) && o.Status == viewType).ToList();
                }
                else
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(p => p.Status == viewType).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(viewType) && string.IsNullOrEmpty(viewDataType))
            {
                #region OLD CODE
                    //if (viewType.ToLower() == "Open Bids".ToLower()  && startDate !=null && endDate !=null)
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                    //                                                                        o.OpenDate <= endDate &&
                    //                                                                        o.ClosedDate == null).ToList();
                    //}
                    //else if (viewType.ToLower() == "Open Bids".ToLower()  && startDate == null && endDate == null)
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.ClosedDate == null).ToList();
                    //}

                    //else if (viewType.ToLower() == "Closed Bids".ToLower()  && startDate !=null && endDate !=null)
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                    //                                                                        o.OpenDate <= endDate &&
                    //                                                                       o.CloseoutDoneDate != null).ToList();
                    //}
                    //else if (viewType.ToLower() == "Closed Bids".ToLower() && startDate == null && endDate == null)
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o =>  o.CloseoutDoneDate != null).ToList();
                    //}


                    //else if (viewType.ToLower() == "Complete Jobs".ToLower() && startDate !=null && endDate !=null)
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                    //                                                                        o.OpenDate <= endDate &&
                    //                                                                       o.PaidInFullDate != null).ToList();
                    //}
                    //else if (viewType.ToLower() == "Complete Jobs".ToLower() && startDate == null && endDate == null)
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.PaidInFullDate != null).ToList();
                    //}


                    //else
                    //{
                    //    dashBoardModel.VDashboardDatum = baseData.Where(o => o.ClosedDate != null).ToList();
                    //}
                    #endregion

                if (startDate != null && endDate != null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                                                                                            o.OpenDate <= endDate &&
                                                                                           o.Status == viewType).ToList();
                }
                else if (viewType.ToLower() == "all".ToLower())
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(p => p.Status != "0").ToList();
                }
                else
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(p => p.Status == viewType).ToList();
                }

            }
            else if (!string.IsNullOrEmpty(viewDataType) && string.IsNullOrEmpty(viewType))
            {
                if (viewDataType.ToLower() == "month".ToLower() && startDate != null && endDate != null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                                                                                        o.OpenDate <= endDate &&
                                                                                        o.OpenDate > DateTime.Now.AddMonths(-1) &&
                                                                                        o.ClosedDate == null).ToList();
                }
                else if (viewDataType.ToLower() == "month".ToLower() && startDate == null && endDate == null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate > DateTime.Now.AddMonths(-1)).ToList();
                }

                else if (viewDataType.ToLower() == "year".ToLower() && startDate != null && endDate != null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                                                                                       o.OpenDate <= endDate &&
                                                                                       o.OpenDate > DateTime.Now.AddYears(-1)).ToList();
                }
                else if ( viewDataType.ToLower() == "year".ToLower() && startDate == null && endDate == null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate > DateTime.Now.AddYears(-1) && o.ClosedDate == null).ToList();
                }

                else if ( viewDataType.ToLower() == "week".ToLower() && startDate !=null && endDate !=null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&
                                                                                        o.OpenDate <= endDate &&
                                                                                        o.OpenDate > DateTime.Now.AddDays(7) &&
                                                                                       o.ClosedDate == null).ToList();
                }
                else if ( viewDataType.ToLower() == "week".ToLower() && startDate == null && endDate == null)
                {
                    dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate > DateTime.Now.AddDays(7) &&  o.ClosedDate == null).ToList();
                }
                else
                {
                    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate > DateTime.Now.AddMonths(-1)).ToList();
                }
            }
            else if ((string.IsNullOrEmpty(viewDataType) && string.IsNullOrEmpty(viewType)) && (startDate != null && endDate != null))
            {
                dashBoardModel.VDashboardDatum = baseData.Where(o => o.OpenDate >= startDate &&  o.OpenDate <= endDate).ToList();
            }
           





                // apply date filters
            dashBoardModel.OpenBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidPrice));
            dashBoardModel.Collected = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.CollectedAmount));
            dashBoardModel.ClosedBids = Convert.ToDecimal(dashBoardModel.VDashboardDatum.Sum(p => p.EagleBidSales));
            dashBoardModel.StartDate = startDate;
            dashBoardModel.EndDate = endDate;
            return dashBoardModel;

            //if (viewType.ToLower() == "Open Bids".ToLower())
            //{
            //    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.OpenDate >= startDate && o.OpenDate <= endDate && o.ClosedDate == null && o.Status != "Rejected").ToList();
            //}
            //else if (viewType.ToLower() == "Closed Bids".ToLower())
            //{
            //    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.ClosedDate != null && o.OpenDate >= startDate && o.OpenDate <= endDate).ToList();
            //}
            //else if (viewType.ToLower() == "Complete Jobs".ToLower())
            //{
            //    dashBoardModel.VDashboardDatum = _context.VDashboardData.Where(o => o.FinishDate != null && o.OpenDate >= startDate && o.OpenDate <= endDate).ToList();
            //}
        }
    }
}
