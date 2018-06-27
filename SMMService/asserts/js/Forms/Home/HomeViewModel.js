var Home = Home || {};

(function () {

    Home.HomeViewModel = function (theParams) {
        theParams = theParams || {};
        if (!theParams.UrlCreateProject || !theParams.UrlProject || !theParams.UrlCreateGroup)
            console.error("В параметры HomeViewModel не переданы все ссылки");

        this.UrlCreateProject = theParams.UrlCreateProject;
        this.UrlProject = theParams.UrlProject;
        this.UrlCreateGroup = theParams.UrlCreateGroup;


        this.NewProject = new Project.ProjectModel();
        this.ErrorCreateProject = ko.observable("");

        this.Projects = ko.observableArray(theParams.Projects ? theParams.Projects.map(function (item) { return new Project.ProjectModel(item) }) : []);

        this.TypeFilter = ko.observable("squad")

        this.NewGroup = new Project.Group.GroupModel();
        this.ErrorCreateGroup = ko.observable("");

        this.Groups = ko.observable(theParams.Groups ? theParams.Groups.map(function (item) { return new Project.Group.GroupModel(item) }) : []);
        return this;
    };

    /**
    * Определяем конструктор
    */
    Home.HomeViewModel.prototype.constructor = Home.HomeViewModel;

    /**
     *Создать проект 
     **/
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
    /**
     *Создать новую группу 
     **/
    Home.HomeViewModel.prototype.CreateGroup = function () {
        var self = this;
        if (this.NewGroup.Name() === "") {
            self.ErrorCreateGroup("Название группы не может быть пустым");
            return false;
        }
        $.post(this.UrlCreateGroup, { name: this.NewGroup.Name() })
            .done(function (response) {
                if (response.IsSuccess) {
                    location.reload()
                }
                else {
                    self.ErrorCreateGroup(response.Message);
                }
            })
            .fail(function (error) {
                console.error(error);
            })
    }

})();