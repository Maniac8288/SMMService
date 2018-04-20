var Calendar = Calendar || {};

(function () {

    Calendar.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.RangeTime = ko.observable(theParams.RangeTime || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Calendar.ViewModel.prototype.constructor = Calendar.ViewModel;


})();