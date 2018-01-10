function ajaxHelperViewModel() {
   ajaxHelperError = ko.observable();

  ajaxHelper = function (uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.ajaxHelperError(errorThrown);
        });
    }

    nonAsyncAjaxHelper = function (uri, method, data) {
        return $.ajax({
            type: method,
            async: false,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? JSON.stringify(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.ajaxHelperError(errorThrown);
        });

    }

    return {
        ajaxHelperError: ajaxHelperError,
        ajaxHelper: ajaxHelper,
        nonAsyncAjaxHelper: nonAsyncAjaxHelper
        }
}