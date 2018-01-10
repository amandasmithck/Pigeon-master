$(document).ready(function () {
    ko.validation.rules.pattern.message = 'Invalid.';

    ko.validation.configure({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });
    var VendorMangementViewModel = function () {

        ko.utils.extend(self, new AjaxHelper());

        //Properties
        numberOfRecords = ko.observable(0);
        isSameAddress = ko.observable(false);
        isSameEmail = ko.observable(false);
        viewCompany = ko.observable(true);
        viewShipping = ko.observable(false);
        viewOEMDealer = ko.observable(false);
        viewInvoice = ko.observable(false);
        isNew = ko.observable(true);
        updateVendorAddressId = ko.observable();

        //checks if the user has completed the form with all required elements
        isCompanyComplete = ko.pureComputed(function () {
            try {
                return this.vendorInfo().companyInfo.isValid();
            }
            catch (err) {
            }

        });
        isInvoiceComplete = ko.pureComputed(function () {
            try {
                return this.vendorInfo().invoiceGuideInfo.isValid();
            }
            catch (err) {
            }

        });
        isShippingComplete = ko.pureComputed(function () {

            try {
                return this.vendorInfo().emptyShippingInfo.isValid();
            }
            catch (err) {
            }

        });

        //isOEMComplete = ko.pureComputed(function () {

        //    try {
        //        return isOEMDealer() ? vendorInfo().oemDealerInfo.isValid() : false;
        //    }
        //    catch (err) {
        //        window.alert(err);
        //    }

        //});
        isOEMDealer = ko.observable(true);
        isEdit = ko.observable(false);
        vendorInfo = ko.observableArray();
        setRequiredElements(true);
        isComplete = ko.pureComputed(function () {
            try {
                return vendorInfo().isValid();
            }
            catch (err) {

            }

        });

        companyClass = ko.pureComputed(function () {
            return isCompanyComplete() ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
        }, VendorMangementViewModel);
        invoiceClass = ko.pureComputed(function () {
            return isInvoiceComplete() ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
        }, VendorMangementViewModel);
        shippingClass = ko.pureComputed(function () {
            return isShippingComplete() ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
        }, VendorMangementViewModel);
        //oemClass = ko.pureComputed(function () {
        //    return isOEMComplete() ? "glyphicon glyphicon-ok" : "glyphicon glyphicon-remove";
        //}, VendorMangementViewModel);


        ko.bindingHandlers.masked = {
            init: function (element, valueAccessor, allBindingsAccessor) {
                var mask = allBindingsAccessor().mask || {};
                $(element).mask(mask);
                ko.utils.registerEventHandler(element, 'blur', function () {
                    var observable = valueAccessor();
                    observable($(element).val());
                });
            },
            update: function (element, valueAccessor) {
                var value = ko.utils.unwrapObservable(valueAccessor());
                $(element).val(value);
            }
        };
       
        //Public Functions
        RefreshNew = function () {
            location.reload();
            //isSameEmail(false);
            //setRequiredElements(true);
        }

        UpdateRequired = function () {

        }

        EditShippingAddress = function (shippingID) {
            updateVendorAddressId(shippingID);
            isEdit(true);
            vendorInfo().emptyShippingInfo.companyID = vendorInfo().companyInfo.companyID;
            StopSpinner();
        }

        VanityShippingOptionToggle = function (vendorShipping) {
            try {
                var controlID = "#vanityOptionList_" + vendorShipping.vendorShippingID;

                if ($(controlID).css("display") == "none") {
                    $(controlID).slideDown();
                }
                else {
                    $(controlID).slideUp();
                }


            }
            catch (err) {
            }
        };




        DeleteShippingAddress = function (shippingID) {

            swal({ text: 'Are you sure you want to Delete This Shipping Address?', type: 'warning', showCancelButton: true, cancelButtonText: "No", confirmButtonText: "Yes", confirmButtonColor: 'green', cancelButtonColor: 'red' }, function (isConfirm) {
                if (isConfirm) {
                    StartSpinner();
                    var url = "/VendorManagementWebService.asmx/DeleteExsitingShippingAddress";
                    var data = { "shippingID": shippingID };
                    self.AjaxCall(url, 'POST', data).done(function (response) {
                        StopSpinner();
                        if (response.d == true) {
                            swal({ text: 'This Address was deleted!', type: 'success' });
                            var i = 0;
                            var indexOfSelectedElement = 0;
                            for (i == 0; i < vendorInfo().exsitingShippingInfo.length; i++) {
                                if (vendorInfo().exsitingShippingInfo[i].vendorShippingID == shippingID) {
                                    indexOfSelectedElement = parseInt(i);
                                    break;
                                }
                            }
                            var vendorData = vendorInfo();
                            var shippingInfo = vendorData.exsitingShippingInfo;
                            
                            shippingInfo.splice(indexOfSelectedElement, 1);
                           

                            vendorData.exsitingShippingInfo = shippingInfo;
                           
                            //update the vendor info uber object with the new data - this should reset the element on the page in the collection
                            vendorInfo(vendorData);
                            
                        }
                        else {
                            swal({ text: 'Something went Wrong', type: 'error' });
                        }

                    });
                }
            })
        }

        AddShippingAddress = function () {
            StartSpinner();
            turnItemIntoNonObservableAgain(true);
            var url = "/VendorManagementWebService.asmx/InsertShippingAddress";
            vendorInfo().emptyShippingInfo.companyID = vendorInfo().companyInfo.companyID;
            var data = { 'shippingInfo': vendorInfo().emptyShippingInfo };
            self.AjaxCall(url, 'POST', data).done(function (response) {
                vendorInfo().exsitingShippingInfo.unshift(response.d[1]);
                vendorInfo().emptyShippingInfo = response.d[0];
                setRequiredShippingElements();
                vendorInfo(vendorInfo());
                StopSpinner();
            });

        }

        MirrorCompany = function (calledFrom) {
            switch (calledFrom) {
                case 'oem':
                    MirrorCompanyOEM();
                    break
                case 'invoice':
                    MirrorCompanyInvoiceGuide();
                    break;
            }
        }

        sameAsCompany = function () {
            if (isSameAddress()) {
                vendorInfo().emptyShippingInfo.address1 = vendorInfo().companyInfo.address1;
                vendorInfo().emptyShippingInfo.address2 = vendorInfo().companyInfo.address2;
                vendorInfo().emptyShippingInfo.city = vendorInfo().companyInfo.city;
                vendorInfo().emptyShippingInfo.state = vendorInfo().companyInfo.state;
                vendorInfo().emptyShippingInfo.zip = vendorInfo().companyInfo.zip;
                vendorInfo().emptyShippingInfo.contact = vendorInfo().companyInfo.contact;
            }
            else {
                vendorInfo().emptyShippingInfo.address1 = "";
                vendorInfo().emptyShippingInfo.address2 = "";
                vendorInfo().emptyShippingInfo.city = "";
                vendorInfo().emptyShippingInfo.state = "";
                vendorInfo().emptyShippingInfo.zip = "";
                vendorInfo().emptyShippingInfo.contact = "";
            }
            vendorInfo(vendorInfo());
            //setRequiredShippingElements();
            //StopSpinner();
        }

        checkEmail = function () {
            if (vendorInfo().companyInfo.email().match(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)) {
                if (vendorInfo().companyInfo.email().substring(vendorInfo().companyInfo.email().indexOf('@') + 1).match(/ckautoparts.com/i) || vendorInfo().companyInfo.email().substring(vendorInfo().companyInfo.email().indexOf('@') + 1).match(/wisinspections.com/i)) {
                    swal({ title: 'Customers Email Address', text: 'Not yours...', type: 'warning' });
                    return false;
                }
                return true;
            }
            swal({ title: 'Invalid Email Address', type: 'warning' });
            //return false;
        }

        GetAllVendors = function () {
            var url = "/VendorManagementWebService.asmx/GetAllVendors"
            self.AjaxCall(url, 'POST', null).done(function (response) {
                vendorInfo().exsitingCompanies = response.d;
                vendorInfo(vendorInfo());
                StopSpinner();
            });
        }

        GetExsitingCompanyData = function () {
            //If it comes back with OEM Stuff then isOEMDealer(true);
            if (vendorInfo().companyInfo.companyID) {
                GetData(vendorInfo().companyInfo.companyID);
            }

        }

        SendData = function () {
            StartSpinner();

            if (!isComplete()) {

                StopSpinner();
                swal({ title: 'Missing Information', type: 'warning', text: 'Please make sure all required fields are filled in. Thank you.' });
                errors.showAllMessages();
                return;
            }

            if (isSameEmail()) {
                vendorInfo().companyInfo.warrantyEmail = vendorInfo().companyInfo.email;
            }
            turnItemIntoNonObservableAgain(false);

            //Send New/Or Updated Vendor
            if (isEdit()) {
                vendorInfo().emptyShippingInfo.companyID = vendorInfo().companyInfo.companyID;
                try
                {
                    var filteredList = vendorInfo().exsitingShippingInfo;
                    var filteredList = filteredList.filter(function(item){
                    return (item.vendorShippingID == updateVendorAddressId());
                    });
                    filteredList[0].companyID = vendorInfo().companyInfo.companyID;
                }
                catch (err) { }

                var url = "/VendorManagementWebService.asmx/editExsitingShippingAddress";
                var data = {'shippingInfo': filteredList[0]};
            }
            else {
                var url = "/VendorManagementWebService.asmx/InsertUpdateVendor";
                var data = { 'vendorInfo': vendorInfo(), 'isNew': isNew(), 'isOEMDealer': isOEMDealer() };
            }
            
            self.AjaxCall(url, 'POST', data).done(function (response) {
                //Refresh Page if an Int >0 comes back
                if (response.d > 0) {
                    StopSpinner();

                    if (isNew()) {
                        swal({ title: 'Vendor Created', type: 'success', text: 'This new Vendors ID is ' + response.d }, function (isConfirm) {
                            setRequiredElements(false);
                        });
                    }
                    else {
                        swal({ title: 'Vendor Updated', type: 'success', text: 'The vendor with ID ' + response.d + ' has been Updated!' }, function (isConfirm) {
                            setRequiredElements(false);
                            GetData(vendorInfo().companyInfo.companyID);
                            isEdit(false);
                        });
                    }

                }
                else {
                    StopSpinner();
                    swal({ title: 'Error', type: 'error', text: 'There was an error processing your request. Please try again. '}, function (isConfirm) {
                        setRequiredElements(true);
                    });
                }

            });
        }

        ViewInvoiceGuide = function () {
            viewInvoice(!viewInvoice());
            viewCompany(false);
            viewShipping(false);
            viewOEMDealer(false);
        }

        ViewCompanyInfo = function () {
            viewCompany(!viewCompany());
            viewShipping(false);
            viewOEMDealer(false);
            viewInvoice(false);
        }

        ViewShippingInfo = function () {
            viewShipping(!viewShipping());
            viewCompany(false);
            viewOEMDealer(false);
            viewInvoice(false);
        }

        ViewOEMDealerInfo = function () {
            viewOEMDealer(!viewOEMDealer());
            viewShipping(false);
            viewCompany(false);
            viewInvoice(false);
        }

        //Private Functions
        function GetData(companyID) {
            StartSpinner();
            var url;
            var data;
            if (isNew()) {
                url = "/VendorManagementWebService.asmx/GetEmptydtoVendor";
                data = null;
            }
            else {
                url = "/VendorManagementWebService.asmx/GetVendorInfoByID";
                data = { 'ID': companyID };
            }

            self.nonAsyncCall(url, 'POST', data).done(function (response) {

                if (!isNew()) {
                    vendorInfo().companyInfo = response.d.companyInfo;
                    vendorInfo().emptyShippingInfo = response.d.emptyShippingInfo;
                    vendorInfo().exsitingCompanies = response.d.exsitingCompanies;
                    vendorInfo().exsitingShippingInfo = response.d.exsitingShippingInfo;
                    vendorInfo().invoiceGuideInfo = response.d.invoiceGuideInfo;
                    vendorInfo().oemDealerInfo = response.d.oemDealerInfo;
                    setRequiredElements(false);
                    vendorInfo(vendorInfo());
                    if (vendorInfo().companyInfo.warrantyEmail() == vendorInfo().companyInfo.email())
                    {
                        isSameEmail(true);
                    }
                    else
                    {
                        isSameEmail(false);
                    }
                    if (vendorInfo().oemDealerInfo.markUpInfo == 0)
                    {
                        isOEMDealer(false);
                    }
                    else
                    {
                        isOEMDealer(true);
                    }
                }
                else {
                    vendorInfo(response.d);
                }
                StopSpinner();
            });
        }
        function turnItemIntoNonObservableAgain(justShipping) {
            if (!justShipping) {
                vendorInfo().invoiceGuideInfo.chosenPaymentTypeID = vendorInfo().invoiceGuideInfo.chosenPaymentTypeID();
                vendorInfo().invoiceGuideInfo.chosenVendorTypeID = vendorInfo().invoiceGuideInfo.chosenVendorTypeID();
                $(vendorInfo().invoiceGuideInfo.allElements).each(function () {
                    if ($(this)[0].elementDataTypeID >= 2) {
                        if (!($(this)[0].elementID == 26 || $(this)[0].elementID == 29 || $(this)[0].elementID == 25)) {
                            $(this)[0].textValue = $(this)[0].textValue();
                        }
                    }
                });
                vendorInfo().companyInfo.active = vendorInfo().companyInfo.active();
                vendorInfo().companyInfo.address1 = vendorInfo().companyInfo.address1();
                vendorInfo().companyInfo.city = vendorInfo().companyInfo.city();
                vendorInfo().companyInfo.company = vendorInfo().companyInfo.company();
                vendorInfo().companyInfo.contact = vendorInfo().companyInfo.contact();
                vendorInfo().companyInfo.email = vendorInfo().companyInfo.email();               
                vendorInfo().companyInfo.warrantyEmail = vendorInfo().companyInfo.warrantyEmail();
                vendorInfo().companyInfo.fax = vendorInfo().companyInfo.fax();
                vendorInfo().companyInfo.phone = vendorInfo().companyInfo.phone();
                vendorInfo().companyInfo.state = vendorInfo().companyInfo.state();
                vendorInfo().companyInfo.type = vendorInfo().companyInfo.type();
                vendorInfo().companyInfo.vendorOrderMethodID = vendorInfo().companyInfo.vendorOrderMethodID();
                vendorInfo().companyInfo.zip = vendorInfo().companyInfo.zip();
                if (isNew()) {
                    if (!isSameAddress()) {
                        vendorInfo().emptyShippingInfo.shippingDirectionTypeID = vendorInfo().emptyShippingInfo.shippingDirectionTypeID();
                        vendorInfo().emptyShippingInfo.vanityName = vendorInfo().emptyShippingInfo.vanityName();
                    }
                    else {
                        vendorInfo().emptyShippingInfo.shippingDirectionTypeID = vendorInfo().emptyShippingInfo.shippingDirectionTypeID();
                        vendorInfo().emptyShippingInfo.vanityName = vendorInfo().emptyShippingInfo.vanityName();
                        vendorInfo().emptyShippingInfo.address1 = vendorInfo().emptyShippingInfo.address1();
                        vendorInfo().emptyShippingInfo.city = vendorInfo().emptyShippingInfo.city();
                        vendorInfo().emptyShippingInfo.contact = vendorInfo().emptyShippingInfo.contact();
                        vendorInfo().emptyShippingInfo.state = vendorInfo().emptyShippingInfo.state();
                        vendorInfo().emptyShippingInfo.zip = vendorInfo().emptyShippingInfo.zip();
                    }
                }
                else if (isEdit()) {
                    vendorInfo().exsitingShippingInfo.shippingDirectionTypeID = vendorInfo().exsitingShippingInfo.shippingDirectionTypeID;
                    vendorInfo().exsitingShippingInfo.vanityName = vendorInfo().exsitingShippingInfo.vanityName;
                    vendorInfo().exsitingShippingInfo.address1 = vendorInfo().exsitingShippingInfo.address1;
                    vendorInfo().exsitingShippingInfo.city = vendorInfo().exsitingShippingInfo.city;
                    vendorInfo().exsitingShippingInfo.contact = vendorInfo().exsitingShippingInfo.contact;
                    vendorInfo().exsitingShippingInfo.state = vendorInfo().exsitingShippingInfo.state;
                    vendorInfo().exsitingShippingInfo.zip = vendorInfo().exsitingShippingInfo.zip;
                }
            }
            else {
                vendorInfo().emptyShippingInfo.shippingDirectionTypeID = vendorInfo().emptyShippingInfo.shippingDirectionTypeID;
                vendorInfo().emptyShippingInfo.vanityName = vendorInfo().emptyShippingInfo.vanityName;
                vendorInfo().emptyShippingInfo.address1 = vendorInfo().emptyShippingInfo.address1;
                vendorInfo().emptyShippingInfo.city = vendorInfo().emptyShippingInfo.city;
                vendorInfo().emptyShippingInfo.contact = vendorInfo().emptyShippingInfo.contact;
                vendorInfo().emptyShippingInfo.state = vendorInfo().emptyShippingInfo.state;
                vendorInfo().emptyShippingInfo.zip = vendorInfo().emptyShippingInfo.zip;
            }
        }
        function setRequiredInvoiceGuideElements(isMirror, response) {
            if (isMirror) {
                response.chosenPaymentTypeID = ko.observable(response.chosenPaymentTypeID).extend({ required: true });
                response.chosenVendorTypeID = ko.observable(response.chosenVendorTypeID).extend({ required: true });
                $(response.allElements).each(function () {
                    if ($(this)[0].elementDataTypeID >= 2) {
                        if (!($(this)[0].elementID == 26 || $(this)[0].elementID == 29 || $(this)[0].elementID == 25)) {
                            $(this)[0].textValue = ko.observable($(this)[0].textValue).extend({ required: true });
                        }
                    }
                });
            }
            else {
                vendorInfo().invoiceGuideInfo.chosenPaymentTypeID = ko.observable(vendorInfo().invoiceGuideInfo.chosenPaymentTypeID).extend({ required: true });
                vendorInfo().invoiceGuideInfo.chosenVendorTypeID = ko.observable(vendorInfo().invoiceGuideInfo.chosenVendorTypeID).extend({ required: true });
                $(vendorInfo().invoiceGuideInfo.allElements).each(function () {
                    if ($(this)[0].elementDataTypeID >= 2) {
                        if (!($(this)[0].elementID == 26 || $(this)[0].elementID == 29 || $(this)[0].elementID == 25)) {
                            $(this)[0].textValue = ko.observable($(this)[0].textValue).extend({ required: true });
                        }
                    }
                });
            }
            invoiceErrors = ko.validation.group(vendorInfo().invoiceGuideInfo, {
                deep: true
            });
            errors = ko.validation.group(vendorInfo(), {
                deep: true
            });
        }
        function SetRequiredCompanyElements() {
            vendorInfo().companyInfo.active = ko.observable(vendorInfo().companyInfo.active).extend({ required: true });
            vendorInfo().companyInfo.address1 = ko.observable(vendorInfo().companyInfo.address1).extend({ required: true });
            vendorInfo().companyInfo.city = ko.observable(vendorInfo().companyInfo.city).extend({ required: true });
            vendorInfo().companyInfo.company = ko.observable(vendorInfo().companyInfo.company).extend({ required: true });
            vendorInfo().companyInfo.contact = ko.observable(vendorInfo().companyInfo.contact).extend({ required: true });
            vendorInfo().companyInfo.email = ko.observable(vendorInfo().companyInfo.email).extend({ required: true });
            vendorInfo().companyInfo.warrantyEmail = ko.observable(vendorInfo().companyInfo.warrantyEmail).extend({ required: false });
            vendorInfo().companyInfo.fax = ko.observable(vendorInfo().companyInfo.fax).extend({ required: false });
            vendorInfo().companyInfo.phone = ko.observable(vendorInfo().companyInfo.phone).extend({ required: true });
            vendorInfo().companyInfo.state = ko.observable(vendorInfo().companyInfo.state).extend({ required: true });
            vendorInfo().companyInfo.type = ko.observable(vendorInfo().companyInfo.type).extend({ required: true });
            vendorInfo().companyInfo.vendorOrderMethodID = ko.observable(vendorInfo().companyInfo.vendorOrderMethodID).extend({ required: true });
            vendorInfo().companyInfo.zip = ko.observable(vendorInfo().companyInfo.zip).extend({ required: true });
            companyErrors = ko.validation.group(vendorInfo().companyInfo, {
                deep: true
            });
        }
        function setRequiredShippingElements() {
            if (isNew()) {
                vendorInfo().emptyShippingInfo.shippingDirectionTypeID = ko.observable(vendorInfo().emptyShippingInfo.shippingDirectionTypeID).extend({ required: true });
                vendorInfo().emptyShippingInfo.vanityName = ko.observable(vendorInfo().emptyShippingInfo.vanityName).extend({ required: true });
            }                   
            else {

                //vendorInfo().emptyShippingInfo.shippingDirectionTypeID = ko.observable(vendorInfo().emptyShippingInfo.shippingDirectionTypeID).extend({ required: true });
                //vendorInfo().emptyShippingInfo.vanityName = ko.observable(vendorInfo().emptyShippingInfo.vanityName).extend({ required: true });
                //vendorInfo().emptyShippingInfo.address1 = ko.observable(vendorInfo().emptyShippingInfo.address1).extend({ required: true });
                //vendorInfo().emptyShippingInfo.city = ko.observable(vendorInfo().emptyShippingInfo.city).extend({ required: true });
                //vendorInfo().emptyShippingInfo.contact = ko.observable(vendorInfo().emptyShippingInfo.contact).extend({ required: true });
                //vendorInfo().emptyShippingInfo.state = ko.observable(vendorInfo().emptyShippingInfo.state).extend({ required: true });
                //vendorInfo().emptyShippingInfo.zip = ko.observable(vendorInfo().emptyShippingInfo.zip).extend({ required: true });
            }
           
            shippingInfo = ko.validation.group(vendorInfo().emptyShippingInfo, {
                deep: true
            });
            errors = ko.validation.group(vendorInfo(), {
                deep: true
            });
        }
        function setRequiredElements(onPageLoad) {
            if (onPageLoad) {
                StartSpinner();
                GetData();
            }
            //InvoiceGuide
            setRequiredInvoiceGuideElements(false, null);
            //CompanyInfo
            SetRequiredCompanyElements();
            //ShippingInfo
            setRequiredShippingElements();
            errors = ko.validation.group(vendorInfo(), {
                deep: true
            });
            this.errors.showAllMessages();
        }

        setOEMRequiredElements = function () {
       
            if (isOEMDealer()) {
                $(vendorInfo().oemDealerInfo.markUpInfo).each(function () {
                    $(this)[0].markUp = ko.observable($(this)[0].markUp).extend({ required: true, min: 1 });
                    $(this)[0].markUpCustomer = ko.observable($(this)[0].markUpCustomer).extend({ required: true, min: 1 });
                });
                oemErrors = ko.validation.group(vendorInfo().oemDealerInfo, {
                    deep: true
                });
            }

        }

        function StartSpinner() {
            var spinTarget = document.getElementById('body');
            spinner = new Spinner(spinnerOpts).spin(spinTarget);
        }

        function StopSpinner() {
            $('.spinner').hide();
        }

        function MirrorCompanyOEM() {
            StartSpinner();
            var url = "/VendorManagementWebService.asmx/MirrorCompanyOEMDealer"
            var data = { 'companyID': vendorInfo().oemDealerInfo.companyToMirror }
            self.AjaxCall(url, 'POST', data).done(function (response) {
                vendorInfo().oemDealerInfo = response.d;
                vendorInfo().exsitingCompanies = vendorInfo().exsitingCompanies;
                vendorInfo(vendorInfo());
                StopSpinner();
            });
        }

        function MirrorCompanyInvoiceGuide() {
            StartSpinner();
            var url = "/VendorManagementWebService.asmx/MirrorCompanyInvoiceGuide"
            var data = { 'companyID': vendorInfo().invoiceGuideInfo.companyToMirror }
            self.AjaxCall(url, 'POST', data).done(function (response) {
                vendorInfo().invoiceGuideInfo = response.d;
                vendorInfo().exsitingCompanies = vendorInfo().exsitingCompanies;
                setRequiredInvoiceGuideElements(true, response.d);
                vendorInfo(vendorInfo());
                StopSpinner();
            });
        }

        return
        {
            numberOfRecords = numberOfRecords,
            viewCompany = viewCompany,
            ViewCompanyInfo = ViewCompanyInfo,
            ViewShippingInfo = ViewShippingInfo,
            ViewOEMDealerInfo = ViewOEMDealerInfo,
            viewShipping = viewShipping,
            updateVendorAddressId = updateVendorAddressId,
            viewOEMDealer = viewOEMDealer,
            SendData = SendData,
            isNew = isNew,
            ViewInvoiceGuide = ViewInvoiceGuide,
            viewInvoice = viewInvoice,
            vendorInfo = vendorInfo,
            AddShippingAddress = AddShippingAddress,
            MirrorCompany = MirrorCompany,
            GetAllVendors = GetAllVendors,
            GetExsitingCompanyData = GetExsitingCompanyData,
            sameAsCompany = sameAsCompany,
            isSameAddress = isSameAddress,
            isSameEmail = isSameEmail,
            EditShippingAddress = EditShippingAddress,
            DeleteShippingAddress = DeleteShippingAddress,
            RefreshNew = RefreshNew,
            isComplete = isComplete,
            isCompanyComplete = isCompanyComplete,
            isInvoiceComplete = isInvoiceComplete,
            isShippingComplete = isShippingComplete,
            isOEMComplete = isOEMComplete,
            isOEMDealer = isOEMDealer,
            isEdit = isEdit,
            checkEmail = checkEmail,
            checkIsComplete = checkIsComplete,
            companyClass = companyClass,
            invoiceClass = invoiceClass,
            oemClass = oemClass,
            shippingClass = shippingClass,
            setOEMRequiredElements = setOEMRequiredElements,
            VanityShippingOptionToggle = VanityShippingOptionToggle
        }
    }
    ko.applyBindings(VendorMangementViewModel());

});

