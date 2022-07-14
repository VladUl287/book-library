export enum BooksMutations {
    UPDATE_BOOK = 'updateBook',
    SET_BOOKS = 'setBooks',
    SET_FILTERS = 'setFilters'
}

export enum BooksActions {
    GET_ALL_BOOKS = 'getAllBooks',
    GET_BOOKS_WITH_FILTERS = 'getBooksWithFilters',
}

export enum AuthMutations {
    SET_AUTH = 'setAuth',
    LOGOUT = 'logout',
}

export enum AuthActions {
    REGISTER = 'register',
    LOGIN = 'login',
    LOGOUT = 'logout',
    REFRESH = 'refresh',
}

export enum BookmarkMutations {
    SET_BOOKMARKS = 'setBookmarks',
}

export enum BookmarkActions {
    GET_BOOKMARKS = 'getBookmarks',
    ADD_BOOKMARK = 'addBookmark',
    REMOVER_BOOKMARK = 'removeBookmark'
}

export enum GenreMutations {
    SET_GENRES = 'setGenres',
}

export enum GenreActions {
    GET_ALL_GENRES = 'getAllGenres',
}