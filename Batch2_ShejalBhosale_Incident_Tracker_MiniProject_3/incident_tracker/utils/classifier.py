import re

network_pattern = re.compile(r'ip|tcp|udp|icmp|switch|vlan|firewall', re.IGNORECASE)
security_pattern = re.compile(r'breach|ransomware|malware|phishing|unauthorized', re.IGNORECASE)
app_pattern = re.compile(r'error|exception|http|timeout|stack trace', re.IGNORECASE)

critical_kw = re.compile(r'outage|down|breach|ransomware|production', re.IGNORECASE)
high_kw = re.compile(r'timeout|failing|unavailable|unreachable', re.IGNORECASE)
medium_kw = re.compile(r'slow|degraded|warning|intermittent', re.IGNORECASE)


def detect_type(text):
    if network_pattern.search(text):
        return "network"
    elif security_pattern.search(text):
        return "security"
    elif app_pattern.search(text):
        return "app"
    return "general"


def detect_severity(text):
    if critical_kw.search(text):
        return "critical"
    elif high_kw.search(text):
        return "high"
    elif medium_kw.search(text):
        return "medium"
    return "low"