import { store } from "..";
import instance from "@/http";
import { Book } from "@/common/contracts";
import { getModule } from 'vuex-module-decorators';
import { Action, Module, Mutation, VuexModule } from "vuex-module-decorators";
import { Guid } from "guid-typescript";

@Module({ dynamic: true, store: store, name: 'bookmarkModule', preserveState: localStorage.getItem('vuex') !== null })
class BookmarksModule extends VuexModule {
    private _bookmarks: Book[] = [];

    get bookmarks(): Book[] {
        return this._bookmarks
    }

    @Mutation
    setBookmarks(bookmarks: Book[]): void {
        this._bookmarks = bookmarks
    }

    @Action
    async getBookmarks(): Promise<void> {
        const result = await instance.get<Book[]>('bookmarks/get')
        this.setBookmarks(result.data);
    }

    @Action
    async addBookmark(bookId: Guid): Promise<void> {
        await instance.post('bookmarks/add/' + bookId)
    }

    @Action
    async removeBookmark(bookId: Guid): Promise<void> {
        await instance.post('bookmarks/remove/' + bookId)
    }
}

export const bookmarksModule = getModule(BookmarksModule, store)