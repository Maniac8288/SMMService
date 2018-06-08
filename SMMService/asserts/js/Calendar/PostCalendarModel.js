var Calendar = Calendar || {};

(function () {

    Calendar.PostCalendarModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        this.DatePublic = ko.observable(theParams.DatePublic || "");
        this.Soical = ko.observable(theParams.Soical || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Calendar.PostCalendarModel.prototype.constructor = Calendar.PostCalendarModel;

    Calendar.PostCalendarModel.prototype.GetDataCalendar = function () {
        return {
            events: [{
                title: this.Name(),
                start: new Date(this.DatePublic()).toLocaleDateString("en-US"), 
                icon: this.Soical()
            }]
        };
    }


})();