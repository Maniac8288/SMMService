var Project = Project || {};

(function () {

    Project.ProjectModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        this.ImageFile = ko.observable(theParams.ImageFile || "");
        this.ImageUrl = ko.observable(theParams.ImageUrl || "");
        this.GroupOk = ko.observable(theParams.GroupOK || "");
        this.GroupVk = ko.observable(theParams.GroupVK || "");
        this.Hashtags = ko.observableArray(theParams.Hashtags ? theParams.Hashtags.map(function (item) { return new Project.HashTagModel(item) }) : []);
        this.GroupId = ko.observable(theParams.GroupId || "");
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
            ImageFile: this.ImageFile()

        }
    }
    /**
     * Установить группу проекту 
     **/
    Project.ProjectModel.prototype.SetGroup = function (id) {
        if (!urlSetGroup) {
            console.error("Сссылка на установку группы отсуствует");
            return false;
        }
        var self = this;
        $.post(urlSetGroup, { projectId: this.Id(), groupId: id }).done(function (res) {
            if (res.IsSuccess)
                self.GroupId(id);
            else
                console.log(res.Message);
        }).fail(function (res) {
            console.error(res);
        });
    }

})();