$('document').ready(function () {
    if(window.location.hash){
        var hash = window.location.hash;
        var id = hash.replace('#','');
        $('.screens > .screen').hide();
        switch(id)
        {
            case "IMS":
              showScreen(1);
              break;
            default:
              $('#welcome').show();
              break;
        }
    } else {
        $('.screens > .screen:not(:first)').hide();
    }
    
    $('.toolbar .button').click(function() {
        var index = $('.toolbar .button').index(this);
        showScreen(index);

        //console.log(index);
    });
    
    function showScreen(index){
        $('.toolbar .button').removeClass('ui-state-selected');
        $('.toolbar .button:nth(' + index + ')').addClass('ui-state-selected');

        $('.screens > .screen').hide();

        $('.screens > .screen:nth('+ (index) +') .subscreen:not(.default)').hide();
        $('.screens > .screen:nth('+ (index) +') .default').show();
        $('.screens > .screen:nth('+ (index) +')').show();

        setTimeout(function(){ window.location.hash = $('.toolbar .button:nth('+index+')').find('span').html() }, 50);
    }

    $('.submenu a').click(function() {
        var index = $('.submenu a').index(this);
        var group = $('.toolbar div.button').index($(this).parents('ul').prev('div'));
        showSubScreen(group, index);

        //console.log(index);
    });

    function showSubScreen(group, index){
        showScreen(group);
        
        $('.screens > .screen:nth('+ (group) +') .subscreen').hide();
        $('.screens > .screen:nth('+ (group) +') .subscreen:nth('+ (index + 1) +')').show();
    }

    function SendAjax(urlMethod, jsonData, returnFunction) {
        $.ajax({
            type: "POST",
            //contentType: 'application/json',
            url: urlMethod,
            data: data,
            //dataType: "json",
            success: function (msg) {
                if (msg != null) {
                    returnFunction(msg);
                }
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                alert(err.Message);
            }
        });
    }

    $('.button').button();

    function start() {

    }

    function end() {

    }

    var data = new Array();
    data[0] = {'Part':'9999', 'Serial':'9999', 'VIN':'XYZ', 'Mileage':'100,000', 'SoldDate':'7/1/2011', 'DateRecv':'7/4/2011', 'ShopName':'Bobs Auto Repair', 'ReturnType':'Core'};

    $("#rms-grid").igGrid({
        autoGenerateColumns: true,
        columns: [
            { headerText: "Part #", key: "Part", dataType: "string" },
            { headerText: "Serial #", key: "Serial", dataType: "string" },
            { headerText: "VIN", key: "VIN", dataType: "string" },
            { headerText: "Mileage", key: "Mileage", dataType: "string" },
            { headerText: "Sold Date", key: "SoldDate", dataType: "string" },
            { headerText: "Date Recv", key: "DateRecv", dataType: "string" },
            { headerText: "Shop Name", key: "ShopName", dataType: "string" },
            { headerText: "Return Type", key: "ReturnType", dataType: "string" }
        ],

        width: "100%",
        dataSource: data,
        //onDataBinding: start,
        //onRendered: end,
        headerCaption: "Inventory",
        //scroll: 'auto',
        //scrollbars: true,
        //height: "100%",
        /*features: [
            {
                name: 'Paging',
                pageSize: 20
            },

            {
                name: 'Filtering',
                enabled: true,
                type: $.ig.Constants.OpType.Local,

                filterTimeout: 0,
                renderFilterButton: true,
                filterDropDownWidth: 250,
                caseSensitive: false,
                filterDropDownItemIcons: false



            },
            {
                name: 'Sorting',
                enabled: true,
                sortUrlKey: 'sort',
                sortUrlAscValueKey: 'asc',
                sortUrlDescValueKey: 'desc',
                mode: $.ig.Constants.SortMode.Single,
                type: $.ig.Constants.OpType.Local,
                columnSettings: [
        			{ columnIndex: 6, allowSorting: true, firstSortDirection: "ascending", currentSortDirection: "descending" }
        		]
            }
        ]*/
    });

    /*$("#rms-grid").bind("iggridcellclick", function (sender, args) {

        if ($(args.cellElement).index() == 0) {
            GetProgram($(args.cellElement).text());
            return false;
        }

    });*/
});