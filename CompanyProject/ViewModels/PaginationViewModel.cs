using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProject.ViewModels
{
    class PaginationViewModel : BaseViewModel
    {
        protected int page;
        public int Page
        {
            get { return page; }
            set { page = value; NotifyPropertyChanged("Page"); }
        }

        protected int pagesize;

        public int PageSize
        {
            get { return pagesize; }
            set { pagesize = value; }
        }

        internal void checkButton()
        {
            if (Page == 1)
            {
                PreviousPageButtonIsEnabled = false;
                FirstPageButtonIsEnabled = false;
            }
            else
            {
                PreviousPageButtonIsEnabled = true;
                FirstPageButtonIsEnabled = true;
            }

            if (Page >= TotalPages)
            {
                if (TotalPages == 0)
                {
                    Page = 0;
                    PreviousPageButtonIsEnabled = false;
                    FirstPageButtonIsEnabled = false;
                }

                NextPageButtonIsEnabled = false;
                LastPageButtonIsEnabled = false;

            }
            else
            {
                NextPageButtonIsEnabled = true;
                LastPageButtonIsEnabled = true;
            }

        }

        protected bool _nextPageButtonIsEnabled;
        public bool NextPageButtonIsEnabled
        {
            get { return _nextPageButtonIsEnabled; }
            set { _nextPageButtonIsEnabled = value; NotifyPropertyChanged("NextPageButtonIsEnabled"); }
        }

        protected bool _lastPageButtonIsEnabled;
        public bool LastPageButtonIsEnabled
        {
            get { return _lastPageButtonIsEnabled; }
            set { _lastPageButtonIsEnabled = value; NotifyPropertyChanged("LastPageButtonIsEnabled"); }
        }

        protected bool _previousPageButtonIsEnabled;
        public bool PreviousPageButtonIsEnabled

        {
            get { return _previousPageButtonIsEnabled; }
            set { _previousPageButtonIsEnabled = value; NotifyPropertyChanged("PreviousPageButtonIsEnabled"); }
        }

        protected bool _firstPageButtonIsEnabled;
        public bool FirstPageButtonIsEnabled

        {
            get { return _firstPageButtonIsEnabled; }
            set { _firstPageButtonIsEnabled = value; NotifyPropertyChanged("FirstPageButtonIsEnabled"); }
        }

        protected int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; NotifyPropertyChanged("TotalPages"); }
        }
        protected string _stringLabelPagina;
        public string StringLabelPagina
        {
            get { return _stringLabelPagina; }
            set { _stringLabelPagina = value; NotifyPropertyChanged("StringLabelPagina"); }
        }
    }
}

