var Project = Project || {};

(function () {

    Project.ProjectModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        this.ImageFile = ko.observable(theParams.ImageFile || "");
        this.ImageUrl = ko.observable(theParams.ImageUrl|| "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.ProjectModel.prototype.constructor = Project.ProjectModel;

    Project.ProjectModel.prototype.GetData = function () {
        return {
            Id: this.Id(),
            Name: this.Name(),
            ImageFile: this.ImageFile(),
            
        }
    }

})();