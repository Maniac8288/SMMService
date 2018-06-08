var Project = Project || {};

(function () {


    Project.ProjectCardsModel = function (theParams) {
        theParams = theParams || {};        
        this.Name = ko.observable(theParams.Name || "");
        this.Info = ko.observable(theParams.Info || "");
        this.Error = ko.observable("");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.ProjectCardsModel.prototype.constructor = Project.ProjectCardsModel;

    Project.ProjectCardsModel.prototype.GetData = function () {
        return {    
            Name: this.Name(),
            Info: this.Info(),     
        }
    }

})();