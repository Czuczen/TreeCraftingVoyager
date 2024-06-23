<template>
    <div v-if="isLoading" class="loader"></div>
    <div class="accordion" id="logsAccordion">
        <div class="accordion-item" v-for="(logs, level) in logsData" :key="level">
            <h2 class="accordion-header" :id="'heading' + level">
                <button class="accordion-button" type="button" data-bs-toggle="collapse" :data-bs-target="'#collapse' + level" aria-expanded="true" :aria-controls="'collapse' + level">
                    {{ level }}
                </button>
            </h2>
            <div :id="'collapse' + level" class="accordion-collapse collapse" aria-labelledby="headingOne" data-bs-parent="#logsAccordion">
                <div class="accordion-body">
                    <div v-for="(log, index) in logs" :key="`log-${level}-${index}`" class="log-entry">
                        <div v-for="(line, lineIndex) in log" :key="`line-${level}-${index}-${lineIndex}`">{{ line }}</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import apiClient from '@/api';

    export default {
        data() {
            return {
                logsData: {},
                isLoading: false
            };
        },
        methods: {
            async fetchLogs() {
                try {
                    this.isLoading = true;
                    apiClient.get('AppManagement/GetLogs')
                        .then(r => r.data)
                        .then(json => {
                            this.logsData = json;
                        })
                        .finally(() => {
                            this.isLoading = false;
                        });
                } catch (error) {
                    console.error('Error fetching logs:', error);
                }
            }
        },
        mounted() {
            this.fetchLogs();
        }
    };
</script>

<style scoped>
    .log-entry {
        border: 1px solid #dee2e6;
        margin-bottom: .5rem;
        padding: .5rem;
        border-radius: .25rem;
    }

    .accordion-body {
        padding: 1rem 0.5rem;
    }

    .accordion-button {
        background-color: #f8f9fa;
        color: #495057;
    }
</style>
