var Project = Project || {};

(function () {

    Project.ProjectInfoViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlSave)
            console.error("Одна из ссылок не была переданна");
        this.UrlSave = theParams.UrlSave;
        this.Project = new Project.ProjectInfoModel(theParams.Project);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Project.ProjectInfoViewModel.prototype.constructor = Project.ProjectInfoViewModel;

    Project.ProjectInfoViewModel.prototype.Save = function () {
        var model = this.Project.GetData();
        $.post(this.UrlSave, model).done(function (res) {
            location.reload();
        }).fail(function (res) {
            console.error(res);
        })
    }
})();