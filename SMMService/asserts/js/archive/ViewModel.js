var Archive = Archive || {};

(function () {

    Archive.ViewModel = function (theParams) {
        theParams = theParams || {};
        this.RangeTime = ko.observable(theParams.RangeTime || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Archive.ViewModel.prototype.constructor = Archive.ViewModel;


})();