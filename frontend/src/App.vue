<template>
  <div id="app">
    <button @click="disconnect" v-if="status === 'connected'">연결끊기</button>
    <button @click="connect" v-if="status === 'disconnected'">연결</button> {{ status }}
    <br /><br />
    <div v-if="status === 'connected'">
      <form @submit.prevent="sendMessage" action="#">
        <input v-model="message"><button type="submit">메세지 전송</button>
      </form>
      <ul id="logs">
        <li v-for="(log, index) in logs" :key="index" class="log">
          {{ log.event }}: {{ log.data }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'App',
  components: {
  },
  data () {
    return {
      message: "",
      logs: [],
      status: "disconnected"
    }
  },
  methods: {
    connect() {
      this.socket = new WebSocket("ws://127.0.0.1:2012");

      this.socket.onopen = () => {
        this.status = "connected";
        this.logs.push({ event: "연결 완료: ", data: 'ws://127.0.0.1:2012'})
        this.socket.onmessage = ({data}) => {
          this.logs.push({ event: "메세지 수신", data });
        };
      };
    },
    disconnect() {
      this.socket.close();
      this.status = "disconnected";
      this.logs = [];
    },
    sendMessage(e) { // eslint-disable-line no-unused-vars
      this.socket.send(this.message);
      this.logs.push({ event: "메시지 전송", data: this.message });
      this.message = "";
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
