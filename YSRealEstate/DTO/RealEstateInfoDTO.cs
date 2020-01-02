using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSRealEstate.DTO
{
    public class RealEstateInfoDTO
    {
        public RealEstateInfoDTO()
        {

        }
        //접수일
        private string number;

        public string 번호
        {
            get { return number; }
            set
            {
                number = value;

            }
        }

        //접수일
        private string receiptDate;

        public string 접수일
        {
            get { return receiptDate; }
            set
            {
                receiptDate = value;
            }
        }

        //계약체결일
        private string contractDate;

        public string 계약체결일
        {
            get { return contractDate; }
            set
            {
                contractDate = value;
            }
        }

        //계약종료일
        private string contractEndDate;

        public string 계약종료일
        {
            get { return contractEndDate; }
            set
            {
                contractEndDate = value;
            }
        }

        //사이즈
        private string spacious;

        public string 평수
        {
            get { return spacious; }
            set
            {
                spacious = value;
            }
        }

        //층
        private string floorNumber;

        public string 층수
        {
            get { return floorNumber; }
            set
            {
                floorNumber = value;
            }
        }

        //구분
        private string estateType;

        public string 매물구분
        {
            get { return estateType; }
            set
            {
                estateType = value;
            }
        }

        //보증금
        private string deposit;

        public string 보증금
        {
            get { return deposit; }
            set
            {
                deposit = value;
            }
        }

        //승강기
        private string elevator;

        public string 승강기
        {
            get { return elevator; }
            set
            {
                elevator = value;
            }
        }

        //호이스트
        private string hoist;

        public string 호이스트
        {
            get { return hoist; }
            set
            {
                hoist = value;
            }
        }

        //층고
        private string floorHeight;

        public string 층고
        {
            get { return floorHeight; }
            set
            {
                floorHeight = value;
            }
        }

        //전력
        private string power;

        public string 전력
        {
            get { return power; }
            set
            {
                power = value;
            }
        }

        //주소
        private string address;

        public string 주소
        {
            get { return address; }
            set
            {
                address = value;
            }
        }

        //담당자 연락처
        private string guestTel;

        public string 담담자연락처
        {
            get { return guestTel; }
            set
            {
                guestTel = value;
            }
        }

        //코멘트
        private string comment;

        public string 비고
        {
            get { return comment; }
            set
            {
                comment = value;
            }
        }        
    }
}
