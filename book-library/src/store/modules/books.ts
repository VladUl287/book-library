import instance from '@/http';
import { Book } from '@/common/contracts';

type BookState = {
    books: Book[]
}

const state: BookState = {
    books: []
};

const getters = {
    Books: (state: BookState) => state.books,
};

const actions = {
    async GetAll({ commit }: any) {
        const result = await instance.get<Book[]>('book/getAll')
        await commit('setBooks', result.data);
    }
};

const mutations = {
    setBooks(state: BookState, data: Book[]) {
        state.books = data
    }
};

export default {
    state,
    getters,
    actions,
    mutations
};