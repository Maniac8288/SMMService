var Layout = Layout || {};

(function () {

    Layout.LayoutViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlGetProjects)
            console.error("Одна из ссылок не была переданна");
        this.UrlGetProjects = theParams.UrlGetProjects;
        this.Projects = ko.observableArray(theParams.Projects ? theParams.Projects.map(function (item) { return new Project.ProjectModel(item) }) : []);
        this.GetProjects();
        return this;
    };

    /**
    * Определяем конструктор
    */
    Layout.LayoutViewModel.prototype.constructor = Layout.LayoutViewModel;

    Layout.LayoutViewModel.prototype.GetProjects = function () {
        var self = this;
        $.post(this.UrlGetProjects).done(function (res) {
            self.Projects(res.map(function (item) { return new Project.ProjectModel(item) }));
        });
    }
})();