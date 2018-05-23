﻿var Post = Post || {};

(function () {

    Post.PostModel = function (theParams) {
        theParams = theParams || {};
        this.Id = ko.observable(theParams.Id || 0);
        this.Name = ko.observable(theParams.Name || "");
        this.Content = ko.observable(theParams.Content || "");
        return this;
    };

    /**
    * Определяем конструктор
    */
    Post.PostModel.prototype.constructor = Post.PostModel;

    Post.PostModel.prototype.GetData = function () {
        return {
            Id: this.Id(),
            Name: this.Name(),
            Content: this.Content()
        };
    }

})();