$(function(){
    /**
     * Validation tabs functionality
     */
    $('#login-form-link').click(function (e) {
        $("#login-form").delay(100).fadeIn(100);
        $("#register-form").fadeOut(100);
        $('.register').removeClass('active');
        $('.login').addClass('active');
        e.preventDefault();
    });
    $('#register-form-link').click(function (e) {
        $("#register-form").delay(100).fadeIn(100);
        $("#login-form").fadeOut(100);
        $('.login').removeClass('active');
        $('.register').addClass('active');
        e.preventDefault();
    });

    /*
    * validate login controls
    */
    $('#login-form')
        .isHappy({
            fields: {
                '#username': {
                    required: true,
                    message: 'Polje korisničko ime ne smije biti prazno'
                },
                '#password': {
                    required: true,
                    message: 'Polje lozinka ne smije biti prazno'
                }
            }
        });

    $('#register-form')
        .isHappy({
            fields: {
                '#Ime': {
                    required: true,
                    message: 'Polje Ime ne smije biti prazno'
                },
                '#Prezime': {
                    required: true,
                    message: 'Polje Prezime ne smije biti prazno'
                },
                '#KorisnickoIme': {
                    required: true,
                    message: 'Polje Korisničko ime ne smije biti prazno'
                },
                '#Adresa': {
                    required: true,
                    message: 'Polje Adresa ne smije biti prazno'
                },
                '#Lozinka': {
                    required: true,
                    message: 'Polje Lozinka ne smije biti prazno'
                },
                '#Email': {
                    required: true,
                    message: 'Polje Email ne smije biti prazno'
                },
                '#Telefon': {
                    required: true,
                    message: 'Polje Telefon ne smije biti prazno'
                }
            }
        });
    //charts
        $('.datepicker').datepicker(); //Initialise any date pickers
        window.PixelAdmin.start(); //initialize pixeladmin theme 

        new Morris.Line({
            // ID of the element in which to draw the chart.
            element: 'monthly-income-graph',
            // Chart data records -- each entry in this array corresponds to a point on
            // the chart.
            data: [
              { year: '2016', value: 20 },
              { year: '2009', value: 10 },
              { year: '2010', value: 5 },
              { year: '2011', value: 5 },
              { year: '2012', value: 20 }
            ],
            // The name of the data record attribute that contains x-values.
            xkey: 'year',
            // A list of names of data record attributes that contain y-values.
            ykeys: ['value'],
            // Labels for the ykeys -- will be displayed when you hover over the
            // chart.
            labels: ['Value']
        });
});