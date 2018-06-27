var Project = Project || {};

(function () {

    Project.HashTagModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Title = ko.observable(theParams.Title || "");
        this.ProjectId = ko.observable(theParams.ProjectId || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.HashTagModel.prototype.constructor = Project.HashTagModel;

    Project.HashTagModel.prototype.GetData = function () {
        return {
            Id: this.Id(),
            Title: this.Title(),
            ProjectId: this.ProjectId()

        }
    }

})();