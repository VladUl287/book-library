import instance from '@/http';
import { Book } from '@/common/contracts';
import { Guid } from 'guid-typescript';

type BookState = {
    books: Book[]
    filters: BooksFilter
}

type BooksFilter = {
    name?: string,
    authorId?: Guid,
    genres?: Guid[],
    rating?: number,
    beginYear?: number,
    endYear?: number
}

const state: BookState = {
    books: [],
    filters: {}
};

const getters = {
    Books: (state: BookState) => state.books,
};

const actions = {
    async GetAll({ commit }: any) {
        const result = await instance.get<Book[]>('book/getAll')
        await commit('setBooks', result.data);
    },
    async GetWithFilters({ commit }: any) {
        const params = getUrlParams(state.filters);
        const result = await instance.get<Book[]>('book/getAll', { params })
        await commit('setBooks', result.data);
    },
    async AddBookmark(_: any, bookId: Guid) {
        await instance.post('bookmark/add/' + bookId.toString())
    },
    async RemoveBookmark(_: any, bookId: Guid) {
        await instance.post('bookmark/remove/' + bookId.toString())
    }
};

const mutations = {
    updateBook(state: BookState, data: Book) {
        const index = state.books.findIndex(x => x.id == data.id);
        if(index) {
            state.books[index] = data;
        }
    },
    setBooks(state: BookState, data: Book[]) {
        state.books = data
    },
    setFilters(state: BookState, data: BooksFilter) {
        state.filters = data;
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};

const getUrlParams = (form: object): URLSearchParams => {
    const init: Record<string, string> = {}
    for (const [key, value] of Object.entries(form)) {
        init[key] = value.toString();
    }
    return new URLSearchParams(init);
}