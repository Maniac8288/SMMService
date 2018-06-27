var Project = Project || {};
Project.Group = Project.Group || {};

(function () {

    Project.Group.GroupModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.Group.GroupModel.prototype.constructor = Project.Group.GroupModel;

    Project.Group.GroupModel.prototype.GetData = function () {
        return {
            Id: this.Id(),
            Name: this.Name(),

        }
    }

})();