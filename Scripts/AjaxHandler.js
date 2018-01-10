function AjaxHelper() {
    
    var self = this;

    self.AjaxCall=function(address, method, data) {
        return $.ajax({
            type: method,
            url: address,
            datatype: 'json',
            contentType: "application/json; charset=utf-8",
            data: data ? JSON.stringify(data) : null,
        }).fail(function (jqXHR, textStatus, errorThrown) {
        });
    }
        self.nonAsyncCall = function(uri, method, data) {
            return $.ajax({
                type: method,
                async: false,
                url: uri,
                dataType: 'json',
                contentType: 'application/json',
                data: data ? JSON.stringify(data) : null
            }).fail(function (jqXHR, textStatus, errorThrown) {
            });
        }
    }
