﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiDichVuModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maLoaiDV;
        private string tenLoaiDV;
        private long donGiaDV;

        public int MaLoaiDV
        {
            get => maLoaiDV;
            set => SetProperty(ref maLoaiDV, value);
        }
        public string TenLoaiDV
        {
            get => tenLoaiDV;
            set => SetProperty(ref tenLoaiDV, value);
        }
        public long DonGiaDV
        {
            get => donGiaDV;
            set => SetProperty(ref donGiaDV, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is LoaiDichVuModel)
            {
                LoaiDichVuModel secondObj = (LoaiDichVuModel)obj;
                //Two service type only match if and only if they both have the same maLoaiDV.
                return (maLoaiDV.Equals(secondObj.maLoaiDV));
            }
            return false;
        }
    }
}
