import Vuex from 'vuex';
import auth from './modules/auth';
import books from './modules/books';
import createPersistedState from "vuex-persistedstate";

export default new Vuex.Store({
    modules: {
      auth,
      books
    },
    plugins: [createPersistedState({
      paths: ['auth', 'books']
    })]
});