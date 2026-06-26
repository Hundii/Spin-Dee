mergeInto(LibraryManager.library, {
    MetaGameComplete: function(str) {
        localStorage.setItem(UTF8ToString(str), new Date().toJSON());
    }
});