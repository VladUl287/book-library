<template>
    <div class="collections-wrap">
        <div v-for="collection in collections">
            {{ collection.name }}
        </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { Collection } from '@/common/contracts'
import { collectionsModule } from '@/store/modules/collections'

const collections = ref<Collection[]>(collectionsModule.collections)

onMounted(async () => {
    await collectionsModule.GetUserCollections()
})

</script>

<style scoped>
</style>