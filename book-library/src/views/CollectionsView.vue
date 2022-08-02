<template>
    <div class="collections-wrap">
        <div v-for="(collection, i) in collectionsModule.collections" :key="i">
            <CollectionCard :collection="collection"/>
        </div>
    </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import { collectionsModule } from '@/store/modules/collections'
import CollectionCard from '@/components/CollectionCard.vue';

onMounted(async () => {
    await collectionsModule.GetUserCollections()
})

</script>

<style scoped>
.collections-wrap {
    width: 75%;
    height: 100%;
    margin: 0 auto;
    padding: 20px 0;
    max-height: 100%;
    overflow-y: auto;
    overflow: visible;
}
</style>