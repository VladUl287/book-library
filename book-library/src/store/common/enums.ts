export enum BooksMutations {
    UPDATE_BOOK = 'updateBook',
    SET_BOOKS = 'setBooks',
    SET_FILTERS = 'setFilters'
}

export enum BooksActions {
    GET_ALL_BOOKS = 'getAllBooks',
    GET_BOOKS_WITH_FILTERS = 'getBooksWithFilters',
    GET_BOOKS_BY_AUTHOR = 'getBooksByAuthor',
    GET_BOOKS_BY_COLLECTION = 'getBookByCollection',
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

export enum CollectionActions {
    GET_ALL_COLLECTIONS = 'getAllCollection',
    GET_USER_COLLECTIONS = 'getUserCollections',
    CREATE_COLLECTION = 'createCollection',
    ADD_BOOK = 'addBook',
    REMOVE_BOOK = 'removeBook'
}

export enum CollectionMutations {
    SET_COLLECTIONS = 'setCollections',
    SET_FILTERS = 'setFilters'
}

export enum ReviewActions {
    GET_ALL_REVIEWS = 'getAllReviews'
}

export enum ReviewMutations {
    SET_REVIEWS = 'setReviews'
}