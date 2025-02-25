﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsLoader.API.Response.Wildberries.reports
{
    internal class IncomesResponse : IWildberriesResponse
    {
        public int Id {  get; set; }
        public int incomeId {get;set;}
        public string number {get;set;}
        public string date {get;set;}
        public string lastChangeDate {get;set;}
        public string supplierArticle {get;set;}
        public string techSize {get;set;}
        public string barcode {get;set;}
        public int quantity {get;set;}
        public int totalPrice {get;set;}
        public string dateClose {get;set;}
        public string warehouseName {get;set;}
        public int nmId {get;set;}
        public string status { get; set; }
    }
}
