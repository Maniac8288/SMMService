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
        this.GroupName = ko.observable(theParams.GroupName || "");
        this.IsFavorite = ko.observable(theParams.IsFavorite || false);
        this.IsArhive = ko.observable(theParams.IsArhive || false);
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
    Project.ProjectModel.prototype.SetGroup = function (id,name) {
        if (!urlSetGroup) {
            console.error("Сссылка на установку группы отсуствует");
            return false;
        }
        var self = this;
        $.post(urlSetGroup, { projectId: this.Id(), groupId: id }).done(function (res) {
            if (res.IsSuccess) {
                self.GroupId(id);            
                self.GroupName(name);
                var eventSetGroup = new CustomEvent('SetGroup', { 'detail': { ProjectId: self.Id(), GroupName: name, GroupId:id } });
                document.dispatchEvent(eventSetGroup)
            }
            else
                console.log(res.Message);
        }).fail(function (res) {
            console.error(res);
        });
    }

    /**
    * Установить  проект в избранные
    **/
    Project.ProjectModel.prototype.SetFavorite = function () {
        if (!urlSetFavorite) {
            console.error("Сссылка на установку в избранные отсуствует");
            return false;
        }
        var self = this;
        $.post(urlSetFavorite, { projectId: this.Id(), isFavorite: !this.IsFavorite() }).done(function (res) {
            if (res.IsSuccess) {
                self.IsFavorite(!self.IsFavorite());
                var eventSetFavorite = new CustomEvent('SetFavorite', { 'detail': { Id: self.Id(), IsFavorite: self.IsFavorite() } });
                document.dispatchEvent(eventSetFavorite)
            }
            else
                console.log(res.Message);
        }).fail(function (res) {
            console.error(res);
        });
    }

})();
