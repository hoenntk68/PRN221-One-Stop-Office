


<template>
  <main>
    <TheWelcome @test-store="testStore" />
    <button>test 2</button>
  </main>
</template>

<script>
import TheWelcome from '../components/TheWelcome.vue'
import { useCounterStore } from '../stores/counter';


export default {
  components: {
    TheWelcome: TheWelcome,
  },
  setup() {
    const counterStore = useCounterStore();
    const { getRequest } = counterStore;

    const testStore = () => {
      getRequest();
    }

    return {
      getRequest,
      testStore,
    }
  }
}
</script>
