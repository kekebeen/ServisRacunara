$(function () {
    /**
     * Validation tabs functionality
     */
    //charts
    $('.datepicker').datepicker(); //Initialise any date pickers
    window.PixelAdmin.start(); //initialize pixeladmin theme 
    var homeZarada = [];
    $.ajax({
        url: '/Administrator/Home/HomeZarada',
        success: function (content) {
            Morris.Bar({
                element: 'monthly-income-graph',
                data: content,
                xkey: 'period',
                postUnits: " KM",
                barColors: ['#A0D468'],
                ykeys: ['value'],
                labels: ['iznos']
            });
        }
    });

    $.ajax({
        url: '/Administrator/Uposlenici/ZaradaIndexJson',
        success: function (content) {
            Morris.Bar({
                element: 'godisnja-zarada',
                data: content,
                xkey: 'period',
                postUnits: " KM",
                xLabelFormat: function (x) {
                    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                    var month = months[x.src.period];
                    return month;
                },
                barColors: ['#A0D468', '#E9573F'],
                ykeys: ['zarada', 'gubitak'],
                labels: ['zarada','gubitak']
            });
        }
    });

});