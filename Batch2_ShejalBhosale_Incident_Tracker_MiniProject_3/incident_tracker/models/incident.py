from utils.classifier import detect_severity

class Incident:
    def __init__(self, data):
        self.id = data["id"]
        self.title = data["title"]
        self.description = data["description"]
        self.reported_by = data["reported_by"]
        self.timestamp = data["timestamp"]
        self.assigned_team = data["assigned_team"]

        self._severity = None
        self.type = None
        self.ticket_ids = {}

    def classify(self):
        raise NotImplementedError("Must implement in subclass")

    @property
    def severity(self):
        return self._severity

    def to_dict(self):
        return {
            "id": self.id,
            "type": self.type,
            "severity": self.severity,
            "team": self.assigned_team
        }

    def __lt__(self, other):
        order = {"critical": 0, "high": 1, "medium": 2, "low": 3}
        return order[self.severity] < order[other.severity]




class NetworkIncident(Incident):
    def classify(self):
        self.type = "network"
        self._severity = detect_severity(self.title + self.description)


class AppIncident(Incident):
    def classify(self):
        self.type = "app"
        self._severity = detect_severity(self.title + self.description)


class SecurityIncident(Incident):
    def classify(self):
        self.type = "security"
        self._severity = detect_severity(self.title + self.description)




def batch_incidents(incidents, size=3):
    for i in range(0, len(incidents), size):
        yield incidents[i:i+size]