﻿using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using Services.Repositories;

namespace Services.Services
{
    public class Core : IDisposable
    {
        private DigiShahrContext _context = new DigiShahrContext();

        private MainRepo<TblDeal> _deal;
        private MainRepo<TblAbility> _ability;
        private MainRepo<TblRole> _role;
        private MainRepo<TblUser> _user;
        private MainRepo<TblNaighborhood> _naighborhood;
        private MainRepo<TblCatagory> _catagory;
        private MainRepo<TblStore> _store;
        private MainRepo<TblDiscount> _discount;
        private MainRepo<TblOrder> _order;
        private MainRepo<TblProduct> _product;
        private MainRepo<TblOrderDetail> _orderDetail;
        private MainRepo<TblStoreNaighborhoodRel> _storeNaighborhoodRel;
        private MainRepo<TblStoreCatagory> _storeCatagory;
        private MainRepo<TblDealOrder> _dealOrder;
        private MainRepo<TblMusic> _music;
        private MainRepo<TblBookMark> _bookmark;
        private MainRepo<TblCity> _city;
        private MainRepo<TblQueue> _queue;

        public MainRepo<TblDeal> Deal => _deal ??= new MainRepo<TblDeal>(_context);
        public MainRepo<TblAbility> Ability => _ability ??= new MainRepo<TblAbility>(_context);
        public MainRepo<TblRole> Role => _role ??= new MainRepo<TblRole>(_context);
        public MainRepo<TblUser> User => _user ??= new MainRepo<TblUser>(_context);
        public MainRepo<TblNaighborhood> Naighborhood => _naighborhood ??= new MainRepo<TblNaighborhood>(_context);
        public MainRepo<TblCatagory> Catagory => _catagory ??= new MainRepo<TblCatagory>(_context);
        public MainRepo<TblStore> Store => _store ??= new MainRepo<TblStore>(_context);
        public MainRepo<TblDiscount> Discount => _discount ??= new MainRepo<TblDiscount>(_context);
        public MainRepo<TblOrder> Order => _order ??= new MainRepo<TblOrder>(_context);
        public MainRepo<TblProduct> Product => _product ??= new MainRepo<TblProduct>(_context);
        public MainRepo<TblOrderDetail> OrderDetail => _orderDetail ??= new MainRepo<TblOrderDetail>(_context);
        public MainRepo<TblStoreNaighborhoodRel> StoreNaighborhoodRel => _storeNaighborhoodRel ??= new MainRepo<TblStoreNaighborhoodRel>(_context);
        public MainRepo<TblStoreCatagory> StoreCatagory => _storeCatagory ??= new MainRepo<TblStoreCatagory>(_context);
        public MainRepo<TblDealOrder> DealOrder => _dealOrder ??= new MainRepo<TblDealOrder>(_context);
        public MainRepo<TblMusic> Music => _music ??= new MainRepo<TblMusic>(_context);
        public MainRepo<TblBookMark> Bookmark => _bookmark ??= new MainRepo<TblBookMark>(_context);
        public MainRepo<TblCity> City => _city ??= new MainRepo<TblCity>(_context);
        public MainRepo<TblQueue> Queue => _queue ??= new MainRepo<TblQueue>(_context);

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
