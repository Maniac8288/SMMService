var Home = Home || {};

(function () {

    Home.HomeViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlCreateProject || !theParams.UrlProject)
            console.error("В параметры HomeViewModel не переданы все ссылки");

        this.UrlCreateProject = theParams.UrlCreateProject;
        this.UrlProject = theParams.UrlProject;


        this.NewProject = new Project.ProjectModel();
        this.ErrorCreateProject = ko.observable("");

        this.Projects = ko.observableArray(theParams.Projects ? theParams.Projects.map(function (item) { return new Project.ProjectModel(item) }) : []);
        console.log(this.Projects() );
        return this;
    };

    /**
    * Определяем конструктор
    */
    Home.HomeViewModel.prototype.constructor = Home.HomeViewModel;

    Home.HomeViewModel.prototype.CreateProject = function () {
        var self = this;
        if (this.NewProject.Name() === "") {
            self.ErrorCreateProject("Название проекта не может быть пустым");
            return false;
        }
        $.post(this.UrlCreateProject, { name: this.NewProject.Name() })
            .done(function (response) {
                if (response.IsSuccess) {
                    window.location = self.UrlProject + '/' + response.Value;
                }
                else {
                    self.ErrorCreateProject(response.Message);
                }
            })
            .fail(function (error) {
                console.error(error);
            })
    }

})();