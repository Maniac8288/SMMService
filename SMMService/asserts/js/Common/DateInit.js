var dateNow = new Date();
var estady = new Date().setDate(new Date().getDate() - 1);
var week = new Date().setDate(new Date().getDate() - 7);
var weekLast = new Date().setDate(new Date().getDate() - 14);
var mounth = new Date().setDate(new Date().getDate() - 30);
var mounthLast = new Date().setDate(new Date().getDate() - 60);
$('#dateRange').daterangepicker({
    "autoApply": true,
    "locale": {
        "format": "MM/DD/YYYY",
        "separator": " - ",
        "applyLabel": "Применить",
        "cancelLabel": "Отмена",
        "fromLabel": "От",
        "toLabel": "До",
        "customRangeLabel": "Свой",
        "daysOfWeek": [
            "Вс",
            "Пн",
            "Вт",
            "Ср",
            "Чт",
            "Пт",
            "Сб"
        ],
        "monthNames": [
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"
        ],
        "firstDay": 1
    },
    "ranges": {
        "Сегодня": [
            dateNow,
            dateNow
        ],
        "Вчера": [
            estady,
            estady

        ],
        "Эта неделя": [
            week,
            dateNow
        ],
        "Прошлая неделя": [
            weekLast,
            week,

        ],
        "Этот месяц": [
            mounth,
            dateNow,

        ],
        "Прошлый месяц": [
            mounthLast,
            mounth,

        ],
        "За все время": [
            "2018-02-28T21:00:00.000Z",
            "2018-03-31T20:59:59.999Z"
        ]
    },
    "linkedCalendars": false,
    "autoUpdateInput": false,
    "alwaysShowCalendars": true,
    "startDate": "04/05/2018",
    "endDate": "04/11/2018"
}, function (start, end, label) {
    ViewModel.RangeTime(start.lang("ru").format('LL') + " - " + end.lang("ru").format('LL'));
});