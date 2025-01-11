#!/bin/bash

# Script accepts three arguments: entity name, folder and a boolean for crud services
ENTITY_NAME=$1
ENTITY_FOLDER=$2
CRUD=$3


# Ensure entity folder exists inside src/Entities
MODEL_DIR="src/Entities/$ENTITY_FOLDER"

if [[ ! -d "$MODEL_DIR" ]]; then
    echo "Error: Entities folder $MODEL_DIR does not exist!"
    exit 1
fi

# Ensure service folder exists inside src/Services
SERVICE_DIR="src/Services/$ENTITY_FOLDER"

if [[ ! -d "$SERVICE_DIR" ]]; then
    echo "Error: Service folder $SERVICE_DIR does not exist!"
    exit 1
fi

# Ensure controller folder exists inside src/Controllers
CONTROLLER_DIR="src/Controllers/$ENTITY_FOLDER"

if [[ ! -d "$MODEL_DIR" ]]; then
    echo "Error: Controllers folder $CONTROLLER_DIR does not exist!"
    exit 1
fi

# Create the files
CONTROLLER_FILE="$CONTROLLER_DIR/${ENTITY_NAME}Controller.cs"
MODEL_FILE="$MODEL_DIR/${ENTITY_NAME}.cs"
INTERFACE_FILE="$SERVICE_DIR/I${ENTITY_NAME}Service.cs"
SERVICE_FILE="$SERVICE_DIR/${ENTITY_NAME}Service.cs"

mkdir -p "$(dirname "$MODEL_FILE")"
mkdir -p "$(dirname "$INTERFACE_FILE")"
mkdir -p "$(dirname "$SERVICE_FILE")"
mkdir -p "$(dirname "$CONTROLLER_FILE")"

# If BaseModel is to be used
if [ "$CRUD" == "true" ]; then

    # Model file
    cat <<EOL > "$MODEL_FILE"
public class ${ENTITY_NAME} : BaseModel
{
    // Additional properties specific to $ENTITY_NAME can be added here
}
EOL

    # Service interface
    cat <<EOL > "$INTERFACE_FILE"
public interface I${ENTITY_NAME}Service : IBaseCrudService<$ENTITY_NAME>
{
    // Additional methods specific to $ENTITY_NAME can be added here
}
EOL

    # Service
    cat <<EOL > "$SERVICE_FILE"
public class ${ENTITY_NAME}Service : CrudService<$ENTITY_NAME>, I${ENTITY_NAME}Service
{
    public ${ENTITY_NAME}Service(BookManagerContext context) : base(context)
    {
    }

    // Add or override custom methods specific to $ENTITY_NAME here
}
EOL

    # Controller
    cat <<EOL > "$CONTROLLER_FILE"
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ${ENTITY_NAME}Controller : CrudController<${ENTITY_NAME}>
{
    public ${ENTITY_NAME}Controller(I${ENTITY_NAME}Service crudService) : base(crudService)
    {
    }
    // Add or override custom methods specific to $ENTITY_NAME here
}


EOL

# No CRUD
else
    # Model file
    cat <<EOL > "$MODEL_FILE"
public class ${ENTITY_NAME}
{
    // Additional properties specific to $ENTITY_NAME can be added here
}
EOL

    # Service interface
    cat <<EOL > "$INTERFACE_FILE"
public interface I${ENTITY_NAME}Service 
{
    // Additional methods specific to $ENTITY_NAME can be added here
}
EOL

    # Service
    cat <<EOL > "$SERVICE_FILE"
public class ${ENTITY_NAME}Service : I${ENTITY_NAME}Service
{
    public ${ENTITY_NAME}Service(BookManagerContext context)
    {
    }

    // Add or override custom methods specific to $ENTITY_NAME here
}
EOL

    # Controller
    cat <<EOL > "$CONTROLLER_FILE"
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ${ENTITY_NAME}Controller : ControllerBase
{
    // Add or override custom methods specific to $ENTITY_NAME here
}
EOL
fi

# Update Program.cs
PROGRAM_FILE="Program.cs"

if [[ ! -f "$PROGRAM_FILE" ]]; then
    echo "Error: Program.cs file not found!"
    exit 1
fi

# Add AddScoped registration to Program.cs
echo "Adding $ENTITY_NAME service to Program.cs..."

# This appends the DI registration line to the Program.cs file
echo "builder.Services.AddScoped<I${ENTITY_NAME}Service, ${ENTITY_NAME}Service>();" >> "$PROGRAM_FILE"

echo "$ENTITY_NAME Service Created and Program.cs Updated!"
