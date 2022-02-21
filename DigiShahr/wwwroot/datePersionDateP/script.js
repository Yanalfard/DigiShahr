$(document).ready(function (){
    $('#date').pDatepicker({
        altField: '#date',
        altFormat: "YYYY/MM/DD",
        observer: true,
        format: 'YYYY/MM/DD',
        initialValue: false,
        initialValueType: 'persian',
        autoClose: true,
        navigator: {
            text: {
                btnNextText: '⬅️',
                btnPrevText: '➡️',
            },
            onNext: function() {
                $('td[data-unix]').each(function (index) {
                    var date = new persianDate(parseInt($(this).attr('data-unix')));
                    date.formatPersian = false;
                    if(holidays.includes(date.format('YYYY/MM/DD')) || date.format('d') == 7) {
                       $(this).find('span').addClass('text-danger');
                    }
                });
            },
            onPrev: function() {
                $('td[data-unix]').each(function (index) {
                    var date = new persianDate(parseInt($(this).attr('data-unix')));
                    date.formatPersian = false;
                    if(holidays.includes(date.format('YYYY/MM/DD')) || date.format('d') == 7) {
                       $(this).find('span').addClass('text-danger');
                    }
                });
            },
        },
        toolbox: {
            calendarSwitch: {
                enabled: false,
            },
            todayButton: {
                text: {
                    fa: 'برو به امروز',
                },
                onToday: function() {
                    $('td[data-unix]').each(function (index) {
                        var date = new persianDate(parseInt($(this).attr('data-unix')));
                        date.formatPersian = false;
                        if(holidays.includes(date.format('YYYY/MM/DD')) || date.format('d') == 7) {
                        $(this).find('span').addClass('text-danger');
                        }
                    });
                },
            },
        },
        onShow: function() {
            $('td[data-unix]').each(function (index) {
                var date = new persianDate(parseInt($(this).attr('data-unix')));
                date.formatPersian = false;
                if(holidays.includes(date.format('YYYY/MM/DD')) || date.format('d') == 7) {
                    $(this).find('span').addClass('text-danger');
                }
            });
        },
    });
});
