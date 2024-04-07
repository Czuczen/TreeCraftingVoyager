<template>
    <div class="logs-viewer">
        <div v-for="(logs, level) in logsData" :key="level" class="accordion-item">
            <h2 class="accordion-header" @click="toggleSection(level)">
                <button class="accordion-button">
                    {{ level }}
                </button>
            </h2>
            <div v-bind:class="{ 'collapse': !expandedSections[level], 'show': expandedSections[level] }" class="accordion-collapse">
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
    export default {
        data() {
            return {
                logsData: {},
                expandedSections: {},
            };
        },
        methods: {
            toggleSection(level) {
                this.expandedSections[level] = !this.expandedSections[level];
            },
            async fetchLogs() {
                try {
                    fetch('/api/AppManagement/GetLogs')
                        .then(r => r.json())
                        .then(json => {
                            this.logsData = json;
                            return;
                        });
                } catch (error) {
                    console.error('Error fetching logs:', error);
                }
            },
        },
        mounted() {
            this.fetchLogs();
        },
    };
</script>

<style scoped>
    .accordion-item {
        margin-bottom: 1rem;
    }

    .accordion-header {
        cursor: pointer;
        padding: 0.5rem 1rem;
        background-color: #f0f0f0;
        border: 1px solid #ddd;
    }

    .accordion-button {
        background: none;
        border: none;
        padding: 0;
        margin: 0;
        width: 100%;
        text-align: left;
    }

    .collapse {
        display: none;
    }

    .show {
        display: block;
    }

    .accordion-body {
        padding: 1rem;
        border: 1px solid #ddd;
        border-top: none;
    }
</style>
