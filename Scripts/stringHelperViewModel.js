
function stringHelperViewModel () {
    
    amIAnInteger = function (int) {
        
        potentialInteger = int.toString();

        try {
            if (potentialInteger == null || potentialInteger == "undefined" || potentialInteger == "") { return false; }

            var numbers = "0123456789";
            var i = 0;
            for (i = 0; i < potentialInteger.length; i++) {
                if (numbers.indexOf(potentialInteger.charAt(i).toString()) == -1) { return false; }
            }

            //all good ... return true
            return true;
        }
        catch (err) { return false; }
    };

    amIADecimal = function (dec) {
        
        try {
            var potentialDecimal = dec.toString();

            if (potentialDecimal == null || potentialDecimal == "undefined" || potentialDecimal == "") { return false; }

            if (potentialDecimal.toString().indexOf("-") == 0) { potentialDecimal = potentialDecimal.substring(1); }

            //so far so good ... there should be no more
            var numbers = "0123456789. ";
            var i = parseInt(0);
            var decimalCount = 0;

            for (i = 0; i < potentialDecimal.toString().length; i++) {

                if (numbers.indexOf(potentialDecimal.charAt(i).toString()) == -1) { return false; }

                if (potentialDecimal.charAt(i) == ".") { decimalCount = decimalCount + 1; }

                if (decimalCount > 1) { return false; }
            }
            return true;

        }
        catch (err) {
            window.alert(err);
            return false;
        }

    };

    amIAlphabetsOnly = function (str) {

        try {
            if (str == null || str == "undefined" || str == "") { return false; }

            var alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var i = 0;
            for (i = 0; i < str.length; i++) {
                if (alphabets.indexOf(str.charAt(i).toString()) == -1) { return false; }
            }

            //all good ... return true
            return true;
        }
        catch (err) { return false; }
    };

    amIAlphabetsOnlyAllowEmptySpace = function (str) {

        try {
            if (str == null || str == "undefined" || str == "") { return false; }

            var alphabets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

            var i = 0;
            for (i = 0; i < str.length; i++) {
                if (alphabets.indexOf(str.charAt(i).toString()) == -1) { return false; }
            }

            //all good ... return true
            return true;
        }
        catch (err) { return false; }
    };

    amIAlphaNumeric = function (str) {

        try {
            if (str == null || str == "undefined" || str == "") { return false; }

            var characterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var i = 0;
            for (i = 0; i < str.length; i++) {
                if (characterSet.indexOf(str.charAt(i).toString()) == -1) { return false; }
            }

            //all good ... return true
            return true;
        }
        catch (err) { return false; }
    };


isNullOrEmpty = function (str) {

        try {
            return (str == null || str == "undefined" || str == "") ? true : false;
        }
        catch (err) { return false; }

    }

    amIAValidEmailAddress = function (emailToTest) {
        try
        {
            
            var atSymbol = emailToTest.indexOf('@');
            if (atSymbol < 1) { return false; }

            var dot = emailToTest.indexOf(".");
            if (dot <= atSymbol + 2) { return false; }

               // check that the dot is not at the end
            if (dot === emailToTest.length - 1) {
                return false;
            }

            return true;
        }
        catch (err)
        { return false; }
       
    }

    hasMinimumCharacters = function (str, count) {
        
        if (str == null || str == "undefined" || str == "") {
            return false;
        }

        return (str.length >= count ? true : false);
    }

    isZero = function (x) {
        try {
            return (((parseInt(x) * 1) + 1) == 1) ? true : false;
        }
        catch (err) {
            window.alert(err);
            return false;
        }

    }

    getValueFromQueryString = function(name) {
        var value = "";

        if (window.location.toString().indexOf("?") == -1) { return value; }

        var queryString = window.location.search.substring(1);

        if (queryString.indexOf(name) == -1) { return value; }

        var qsArray = new Array();
        var i = parseInt("0");

        qsArray = queryString.split("&");
        if (qsArray == null || qsArray.length == 0) { return value; }

        for (i = 0; i < qsArray.length; i++) {
            var nvp = new Array();
            nvp = qsArray[i].split("=");
            if (nvp[0] == name) {
                value = nvp[1];
                break;
            }
        }

        return value;
    }

    amIPhoneNumber = function (potentialPhone) {
        try { return (isNullOrEmpty(potentialPhone) || !amIAnInteger(potentialPhone) || potentialPhone.length < 10 ? false : true); }
        catch (err) { return false; }
    };

    amIAUSPostalCode = function (potentialZip) {
        try { return (isNullOrEmpty(potentialZip) || !amIAnInteger(potentialZip) || potentialZip.length < 5 ? false : true); }
        catch (err) { return false; }
    };

    amIADate = function (potentialDate) {
        try { return (isNullOrEmpty(potentialDate) ? false : true); }
        catch (err) { return false; }
    };

    amIAnEmail = function (potentialEmail) {
        try { return (isNullOrEmpty(potentialEmail) ? false : true); }
        catch (err) { return false; }
    };
    
    return {
            amIAnInteger : amIAnInteger,
            amIADecimal : amIADecimal,
            amIAlphabetsOnly : amIAlphabetsOnly,
            amIAlphabetsOnlyAllowEmptySpace : amIAlphabetsOnlyAllowEmptySpace,
            amIAlphaNumeric : amIAlphaNumeric,
            isNullOrEmpty : isNullOrEmpty,
            amIAValidEmailAddress : amIAValidEmailAddress,
            hasMinimumCharacters : hasMinimumCharacters,
            isZero : isZero,
            getValueFromQueryString : getValueFromQueryString,
            amIPhoneNumber : amIPhoneNumber,
            amIAUSPostalCode : amIAUSPostalCode,
            amIADate : amIADate,
            amIAnEmail : amIAnEmail
        }
};

