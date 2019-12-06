<template>
  <div class="home">
    <Extrato></Extrato>
    <hr>
    <Cotacao></Cotacao>
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld';
import Extrato from '@/components/Extrato';
import Cotacao from '@/components/Cotacao';

export default {
  name: 'home',
  components: {
    HelloWorld,
    Extrato,
    Cotacao
  }
}
</script>

<style scoped>
.home {
  margin: 0% 15%;
  padding: 20px 0;
  border: 1px solid rgb(214, 209, 209);
}
</style>
