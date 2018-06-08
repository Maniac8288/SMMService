var Calendar = Calendar || {};

(function () {

    Calendar.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.RangeTime = ko.observable(theParams.RangeTime || "");
        this.Posts = ko.observableArray(theParams.Posts ? theParams.Posts.map(function (item) { return new Calendar.PostCalendarModel(item) }) : []);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Calendar.ViewModel.prototype.constructor = Calendar.ViewModel;


})();