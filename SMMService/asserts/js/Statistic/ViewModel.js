var Statistic = Statistic || {};

(function () {

    Statistic.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.IsPost = ko.observable(true);
        this.RangeTime = ko.observable(theParams.RangeTime || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Statistic.ViewModel.prototype.constructor = Statistic.ViewModel;

   
})();