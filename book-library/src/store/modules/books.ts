import { store } from "..";
import instance from "@/http";
import { Book } from "@/common/contracts";
import { Module, VuexModule, Mutation, Action, getModule } from "vuex-module-decorators";

@Module({ dynamic: true, store: store, name: 'booksModule', preserveState: localStorage.getItem('vuex') !== null })
class BooksModule extends VuexModule {
    private _books: Book[] = []

    get books(): Book[] {
        return this._books
    }

    @Mutation
    setBooks(books: Book[]) {
        this._books = books
    }

    @Action({ rawError: true })
    async getBooks() {
        const result = await instance.get<Book[]>('book/getAll')      
        this.setBooks(result.data)
    }
}

export const booksModule = getModule(BooksModule, store)