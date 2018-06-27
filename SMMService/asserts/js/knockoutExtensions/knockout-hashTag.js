ko.bindingHandlers.hashTag = {
    update: function (element, valueAccessor, allBindings, viewModel) {
        var value = valueAccessor();
        if (value().length == 0) {
            value("#");
        }
    }
};